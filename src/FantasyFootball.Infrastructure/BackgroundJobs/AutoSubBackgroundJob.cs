
namespace FantasyFootball.Infrastructure.BackgroundJobs
{
    public class AutoSubBackgroundJob : IAutoSubJob
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AutoSubBackgroundJob> _logger;

        public AutoSubBackgroundJob(
            IFantasyTeamRepository fantasyTeamRepository,
            IFixtureRepository fixtureRepository,
            IPlayerEventRepository playerEventRepository,
            IUnitOfWork unitOfWork,
            ILogger<AutoSubBackgroundJob> logger)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _fixtureRepository = fixtureRepository;
            _playerEventRepository = playerEventRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task ProcessAutoSubsAsync(Guid gameweekId)
        {
            _logger.LogInformation("[HANGFIRE JOB STARTED] Starting Auto-Substitutions for Gameweek {GameweekId}", gameweekId);

            var fixtures = await _fixtureRepository.GetByGameweekIdAsync(gameweekId);
            var fixtureIds = fixtures.Select(f => f.Id).ToList();

            var allGameweekEvents = new List<Domain.Entities.PlayerEvent>();
            foreach (var fId in fixtureIds)
            {
                var evts = await _playerEventRepository.GetByFixtureIdAsync(fId);
                allGameweekEvents.AddRange(evts);
            }

            bool DidPlayerPlay(Guid playerId)
            {
                return allGameweekEvents.Any(e => e.PlayerId == playerId && e.EventType == EventType.MinutesPlayed);
            }

            var allTeams = await _fantasyTeamRepository.GetAllAsync();
            var teamsWithAutoSub = allTeams.Where(t => t.AutoSubEnabled).Select(t => t.Id).ToList();

            _logger.LogInformation("Found {Count} teams with AutoSubEnabled = true.", teamsWithAutoSub.Count);

            foreach (var teamId in teamsWithAutoSub)
            {
                var team = await _fantasyTeamRepository.GetByIdWithPlayersAsync(teamId);
                if (team == null) continue;

                var startingXI = team.Players.Where(p => !p.IsOnBench).ToList();
                var didNotPlay = startingXI.Where(p => !DidPlayerPlay(p.PlayerId)).ToList();

                if (!didNotPlay.Any()) continue;
                
                _logger.LogInformation("Processing Auto-Sub for Team {TeamName} (Id: {TeamId})...", team.Name, team.Id);

                var benchPlayers = team.Players.Where(p => p.IsOnBench).ToList();

                foreach (var playerOut in didNotPlay)
                {
                    var isGk = playerOut.Player?.Position == PlayerPosition.Goalkeeper;
                    
                    var potentialSubs = benchPlayers
                        .Where(p => DidPlayerPlay(p.PlayerId))
                        .Where(p => isGk ? p.Player?.Position == PlayerPosition.Goalkeeper : p.Player?.Position != PlayerPosition.Goalkeeper)
                        .ToList();

                    foreach (var subIn in potentialSubs)
                    {
                        var outPos = playerOut.Player?.Position;
                        var inPos = subIn.Player?.Position;

                        if (outPos == inPos)
                        {
                            team.SubstitutePlayer(subIn.PlayerId, playerOut.PlayerId);
                            benchPlayers.Remove(subIn);
                            startingXI.Remove(playerOut);
                            startingXI.Add(subIn);
                            _logger.LogInformation("Auto-Subbed IN {SubInId} for OUT {SubOutId}", subIn.PlayerId, playerOut.PlayerId);
                            break;
                        }

                        var defs = startingXI.Count(p => p.Player?.Position == PlayerPosition.Defender);
                        var mids = startingXI.Count(p => p.Player?.Position == PlayerPosition.Midfielder);
                        var fwds = startingXI.Count(p => p.Player?.Position == PlayerPosition.Forward);

                        if (outPos == PlayerPosition.Defender) defs--;
                        else if (outPos == PlayerPosition.Midfielder) mids--;
                        else if (outPos == PlayerPosition.Forward) fwds--;

                        if (inPos == PlayerPosition.Defender) defs++;
                        else if (inPos == PlayerPosition.Midfielder) mids++;
                        else if (inPos == PlayerPosition.Forward) fwds++;

                        if (defs >= 3 && mids >= 2 && fwds >= 1)
                        {
                            team.SubstitutePlayer(subIn.PlayerId, playerOut.PlayerId);
                            benchPlayers.Remove(subIn);
                            startingXI.Remove(playerOut);
                            startingXI.Add(subIn);
                            _logger.LogInformation("Auto-Subbed IN {SubInId} for OUT {SubOutId}", subIn.PlayerId, playerOut.PlayerId);
                            break;
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(default);
            _logger.LogInformation("[HANGFIRE JOB COMPLETED] Auto-Substitutions for Gameweek {GameweekId} finished successfully.", gameweekId);
        }
    }
}
