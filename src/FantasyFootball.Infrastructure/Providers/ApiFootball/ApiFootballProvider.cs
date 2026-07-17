namespace FantasyFootball.Infrastructure.Providers.ApiFootball;

public class ApiFootballProvider : IFootballDataProvider
{
    private readonly ApiFootballClient _client;
    private readonly AppDbContext _context;
    private readonly TeamMapper _teamMapper;
    private readonly PlayerMapper _playerMapper;
    private readonly FixtureMapper _fixtureMapper;
    private readonly ApiFootballOptions _options;
    private readonly ILogger<ApiFootballProvider> _logger;

    public ApiFootballProvider(
        ApiFootballClient client,
        AppDbContext context,
        TeamMapper teamMapper,
        PlayerMapper playerMapper,
        FixtureMapper fixtureMapper,
        IOptions<ApiFootballOptions> options,
        ILogger<ApiFootballProvider> logger)
    {
        _client = client;
        _context = context;
        _teamMapper = teamMapper;
        _playerMapper = playerMapper;
        _fixtureMapper = fixtureMapper;
        _options = options.Value;
        _logger = logger;
    }

    public async Task SyncPlayerInjuriesAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteSyncAsync("Injuries", async (correlationId) =>
        {
            string endpoint = ApiFootballEndpoints.Injuries(_options.LeagueId, _options.Season);
            var response = await FetchFromApi<FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Injuries.ApiInjuryResponseDto>(endpoint, cancellationToken);

            var existingPlayers = await _context.Players.ToListAsync(cancellationToken);
            int updated = 0;

            // Reset all players injuries first (optional, but good for tracking current state)
            foreach (var p in existingPlayers) 
            {
                if (p.Status == FantasyFootball.Domain.Enums.AvailabilityStatus.Injured)
                {
                    p.UpdateAvailability(FantasyFootball.Domain.Enums.AvailabilityStatus.Available, 100);
                }
            }

            foreach (var dto in response.Response)
            {
                var existingPlayer = existingPlayers.FirstOrDefault(p => p.ExternalId == dto.Player.Id);
                if (existingPlayer != null)
                {
                    existingPlayer.UpdateAvailability(FantasyFootball.Domain.Enums.AvailabilityStatus.Injured, 0);
                    updated++;
                }
            }
            return (0, updated);
        }, cancellationToken);
    }

    public async Task SyncMatchEventsAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteSyncAsync("MatchEvents", async (correlationId) =>
        {
            // For MVP: Fetch fixtures that are happening today or recently finished to get events
            var recentFixtures = await _context.Fixtures
                .Where(f => f.KickOff >= DateTime.UtcNow.AddDays(-1) && f.KickOff <= DateTime.UtcNow.AddHours(2))
                .ToListAsync(cancellationToken);

            var players = await _context.Players.Where(p => p.ExternalId != null).ToListAsync(cancellationToken);
            int inserted = 0;

            foreach (var fixture in recentFixtures)
            {
                if (fixture.ExternalId == null) continue;

                string endpoint = ApiFootballEndpoints.FixtureEvents(fixture.ExternalId.Value);
                var response = await FetchFromApi<FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Events.ApiEventResponseDto>(endpoint, cancellationToken);

                // For simplicity in MVP, we just load existing events to avoid duplicates
                // In production, we'd only add NEW events based on their time/id
                var existingEvents = await _context.PlayerEvents.Where(e => e.FixtureId == fixture.Id).ToListAsync(cancellationToken);

                foreach (var dto in response.Response)
                {
                    var player = players.FirstOrDefault(p => p.ExternalId == dto.Player?.Id);
                    if (player == null) continue;

                    FantasyFootball.Domain.Enums.EventType? mappedType = dto.Type.ToLower() switch
                    {
                        "goal" when dto.Detail.ToLower() == "normal goal" => FantasyFootball.Domain.Enums.EventType.Goal,
                        "goal" when dto.Detail.ToLower() == "own goal" => FantasyFootball.Domain.Enums.EventType.OwnGoal,
                        "goal" when dto.Detail.ToLower() == "penalty" => FantasyFootball.Domain.Enums.EventType.Goal,
                        "goal" when dto.Detail.ToLower() == "missed penalty" => FantasyFootball.Domain.Enums.EventType.PenaltyMiss,
                        "card" when dto.Detail.ToLower() == "yellow card" => FantasyFootball.Domain.Enums.EventType.YellowCard,
                        "card" when dto.Detail.ToLower() == "red card" => FantasyFootball.Domain.Enums.EventType.RedCard,
                        _ => null
                    };

                    if (mappedType.HasValue)
                    {
                        // Check if event already logged (naive check by type for MVP)
                        // In reality, players can score multiple goals, so we should check event ID from API if available
                        // We will allow duplicates for MVP if re-syncing doesn't clear them, so we clear old ones first:
                        if (!existingEvents.Any(e => e.PlayerId == player.Id && e.EventType == mappedType.Value))
                        {
                            var playerEvent = FantasyFootball.Domain.Entities.PlayerEvent.Create(player.Id, fixture.Id, mappedType.Value, 0);
                            _context.PlayerEvents.Add(playerEvent);
                            existingEvents.Add(playerEvent);
                            inserted++;
                        }
                    }

                    // Assist
                    if (dto.Type.ToLower() == "goal" && dto.Assist?.Id != null)
                    {
                        var assistPlayer = players.FirstOrDefault(p => p.ExternalId == dto.Assist.Id);
                        if (assistPlayer != null)
                        {
                            if (!existingEvents.Any(e => e.PlayerId == assistPlayer.Id && e.EventType == FantasyFootball.Domain.Enums.EventType.Assist))
                            {
                                var assistEvent = FantasyFootball.Domain.Entities.PlayerEvent.Create(assistPlayer.Id, fixture.Id, FantasyFootball.Domain.Enums.EventType.Assist, 0);
                                _context.PlayerEvents.Add(assistEvent);
                                existingEvents.Add(assistEvent);
                                inserted++;
                            }
                        }
                    }
                }

                // ALSO Fetch Match Statistics (Minutes, Saves, CleanSheets)
                string statsEndpoint = ApiFootballEndpoints.FixturePlayers(fixture.ExternalId.Value);
                var statsResponse = await FetchFromApi<FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures.ApiFixturePlayersResponseDto>(statsEndpoint, cancellationToken);

                foreach (var teamStats in statsResponse.Response)
                {
                    foreach (var pStat in teamStats.Players)
                    {
                        var player = players.FirstOrDefault(p => p.ExternalId == pStat.Player.Id);
                        if (player == null) continue;

                        var stat = pStat.Statistics.FirstOrDefault();
                        if (stat == null) continue;

                        // Minutes Played
                        int minutes = stat.Games?.Minutes ?? 0;
                        if (minutes > 0 && !existingEvents.Any(e => e.PlayerId == player.Id && e.EventType == FantasyFootball.Domain.Enums.EventType.MinutesPlayed))
                        {
                            int pts = minutes >= 60 ? 2 : 1;
                            var minEvent = FantasyFootball.Domain.Entities.PlayerEvent.Create(player.Id, fixture.Id, FantasyFootball.Domain.Enums.EventType.MinutesPlayed, pts);
                            _context.PlayerEvents.Add(minEvent);
                            existingEvents.Add(minEvent);
                            inserted++;
                        }

                        // Saves
                        int saves = stat.Goals?.Saves ?? 0;
                        if (saves >= 3 && !existingEvents.Any(e => e.PlayerId == player.Id && e.EventType == FantasyFootball.Domain.Enums.EventType.Saves))
                        {
                            int pts = saves / 3; // 1 point for every 3 saves
                            var saveEvent = FantasyFootball.Domain.Entities.PlayerEvent.Create(player.Id, fixture.Id, FantasyFootball.Domain.Enums.EventType.Saves, pts);
                            _context.PlayerEvents.Add(saveEvent);
                            existingEvents.Add(saveEvent);
                            inserted++;
                        }

                        // Clean Sheet
                        int conceded = stat.Goals?.Conceded ?? 0;
                        if (minutes >= 60 && conceded == 0 && !existingEvents.Any(e => e.PlayerId == player.Id && e.EventType == FantasyFootball.Domain.Enums.EventType.CleanSheet))
                        {
                            var csEvent = FantasyFootball.Domain.Entities.PlayerEvent.Create(player.Id, fixture.Id, FantasyFootball.Domain.Enums.EventType.CleanSheet, 0);
                            _context.PlayerEvents.Add(csEvent);
                            existingEvents.Add(csEvent);
                            inserted++;
                        }
                    }
                }

                await Task.Delay(500, cancellationToken); // Rate limiting
            }
            return (inserted, 0);
        }, cancellationToken);
    }

    public async Task SyncStandingsAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteSyncAsync("Standings", async (correlationId) =>
        {
            string endpoint = ApiFootballEndpoints.Standings(_options.LeagueId, _options.Season);
            var response = await FetchFromApi<FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings.ApiStandingResponseDto>(endpoint, cancellationToken);

            var existingTeams = await _context.Teams.ToListAsync(cancellationToken);
            int updated = 0;

            if (response.Response.FirstOrDefault()?.League.Standings.FirstOrDefault() is List<FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings.ApiStandingDto> standingsList)
            {
                foreach (var dto in standingsList)
                {
                    var team = existingTeams.FirstOrDefault(t => t.ExternalId == dto.Team.Id);
                    if (team != null)
                    {
                        team.UpdateStandings(
                            dto.Rank, 
                            dto.All.Played, 
                            dto.All.Win, 
                            dto.All.Draw, 
                            dto.All.Lose, 
                            dto.All.Goals.For, 
                            dto.All.Goals.Against, 
                            dto.GoalsDiff, 
                            dto.Points);
                        updated++;
                    }
                }
            }

            return (0, updated);
        }, cancellationToken);
    }

    public async Task SyncTeamsAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteSyncAsync("Teams", async (correlationId) =>
        {
            string endpoint = ApiFootballEndpoints.Teams(_options.LeagueId, _options.Season);
            var response = await FetchFromApi<ApiTeamResponseDto>(endpoint, cancellationToken);

            var existingTeams = await _context.Teams.ToListAsync(cancellationToken);
            int inserted = 0, updated = 0;

            foreach (var dto in response.Response)
            {
                var existingTeam = existingTeams.FirstOrDefault(t => t.ExternalId == dto.Team.Id);
                if (existingTeam != null)
                {
                    _teamMapper.UpdateEntity(existingTeam, dto);
                    updated++;
                }
                else
                {
                    _context.Teams.Add(_teamMapper.Map(dto));
                    inserted++;
                }
            }
            return (inserted, updated);
        }, cancellationToken);
    }

    public async Task SyncPlayersAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteSyncAsync("Players", async (correlationId) =>
        {
            int page = 1;
            int totalPages = 1;
            int inserted = 0, updated = 0;

            var teams = await _context.Teams.Where(t => t.ExternalId != null).ToListAsync(cancellationToken);
            var existingPlayers = await _context.Players.ToListAsync(cancellationToken);

            do
            {
                string endpoint = ApiFootballEndpoints.Players(_options.LeagueId, _options.Season, page);
                ApiFootballResponse<ApiPlayerResponseDto> response;
                try
                {
                    response = await FetchFromApi<ApiPlayerResponseDto>(endpoint, cancellationToken);
                }
                catch (Exception ex) when (ex.Message.Contains("maximum value of 3 for the Page parameter") || ex.Message.Contains("Free plans"))
                {
                    // Gracefully stop if we hit the Free Plan limit
                    break;
                }

                if (response.Paging != null && response.Paging.Total > 0)
                {
                    totalPages = response.Paging.Total;
                }

                foreach (var dto in response.Response)
                {
                    var teamIdStr = dto.Statistics.FirstOrDefault()?.Team.Id;
                    if (teamIdStr == null) continue;

                    var dbTeam = teams.FirstOrDefault(t => t.ExternalId == teamIdStr);
                    if (dbTeam == null) continue;

                    var existingPlayer = existingPlayers.FirstOrDefault(p => p.ExternalId == dto.Player.Id);
                    if (existingPlayer != null)
                    {
                        _playerMapper.UpdateEntity(existingPlayer, dto, dbTeam.Id);
                        updated++;
                    }
                    else
                    {
                        _context.Players.Add(_playerMapper.Map(dto, dbTeam.Id));
                        inserted++;
                    }
                }

                page++;
                if (page <= totalPages) 
                {
                    await Task.Delay(500, cancellationToken); // Rate limiting protection
                }
            } while (page <= totalPages);

            return (inserted, updated);
        }, cancellationToken);
    }

    public async Task SyncFixturesAsync(CancellationToken cancellationToken = default)
    {
        await ExecuteSyncAsync("Fixtures", async (correlationId) =>
        {
            string endpoint = ApiFootballEndpoints.Fixtures(_options.LeagueId, _options.Season);
            var response = await FetchFromApi<ApiFixtureResponseDto>(endpoint, cancellationToken);

            var teams = await _context.Teams.Where(t => t.ExternalId != null).ToListAsync(cancellationToken);
            var gameweeks = await _context.Gameweeks.ToListAsync(cancellationToken);
            var existingFixtures = await _context.Fixtures.ToListAsync(cancellationToken);
            int inserted = 0, updated = 0;

            foreach (var dto in response.Response)
            {
                var homeTeam = teams.FirstOrDefault(t => t.ExternalId == dto.Teams.Home.Id);
                var awayTeam = teams.FirstOrDefault(t => t.ExternalId == dto.Teams.Away.Id);

                if (homeTeam == null || awayTeam == null) continue;

                var roundName = !string.IsNullOrEmpty(dto.League?.Round) ? dto.League.Round : "1";
                int roundNumber = 1;
                var match = System.Text.RegularExpressions.Regex.Match(roundName, @"\d+");
                if (match.Success)
                {
                    roundNumber = int.Parse(match.Value);
                }

                var gameweek = gameweeks.FirstOrDefault(g => g.Number == roundNumber);
                if (gameweek == null)
                {
                    gameweek = global::Gameweek.Create(roundNumber, DateTime.UtcNow.AddDays(7));
                    _context.Gameweeks.Add(gameweek);
                    gameweeks.Add(gameweek);
                }

                var existingFixture = existingFixtures.FirstOrDefault(f => f.ExternalId == dto.Fixture.Id);
                if (existingFixture != null)
                {
                    _fixtureMapper.UpdateEntity(existingFixture, dto);
                    updated++;
                }
                else
                {
                    _context.Fixtures.Add(_fixtureMapper.Map(dto, homeTeam.Id, awayTeam.Id, gameweek.Id));
                    inserted++;
                }
            }
            return (inserted, updated);
        }, cancellationToken);
    }

    private async Task<ApiFootballResponse<T>> FetchFromApi<T>(string endpoint, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync<ApiFootballResponse<T>>(endpoint, cancellationToken);
        var errorsStr = response?.Errors?.ToString() ?? "";
        var cleanErrors = errorsStr.Replace(" ", "").Replace("\n", "").Replace("\r", "");
        bool hasErrors = !string.IsNullOrEmpty(cleanErrors) && cleanErrors != "{}" && cleanErrors != "[]";
        if (response == null || hasErrors)
        {
            throw new Exception($"API Error: {errorsStr}");
        }
        return response;
    }

    private async Task ExecuteSyncAsync(string syncType, Func<Guid, Task<(int Inserted, int Updated)>> syncAction, CancellationToken cancellationToken)
    {
        var correlationId = Guid.NewGuid();
        var syncHistory = DataSyncHistory.Start("ApiFootball", syncType, correlationId);
        _context.DataSyncHistories.Add(syncHistory);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Starting {SyncType} Sync via ApiFootballProvider... [CorrelationId: {CorrelationId}]", syncType, correlationId);

        try
        {
            var (inserted, updated) = await syncAction(correlationId);

            await _context.SaveChangesAsync(cancellationToken);

            syncHistory.Complete(inserted, updated);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("{SyncType} Sync completed successfully. Inserted: {Inserted}, Updated: {Updated}", syncType, inserted, updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{SyncType} Sync failed. [CorrelationId: {CorrelationId}]", syncType, correlationId);
            syncHistory.Fail(ex.Message);
            await _context.SaveChangesAsync(cancellationToken);
            throw;
        }
    }
}
