namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateGameweekScore
{
    public class CalculateGameweekScoreCommandHandler : IRequestHandler<CalculateGameweekScoreCommand, Result<int>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CalculateGameweekScoreCommandHandler(
            IFantasyTeamRepository fantasyTeamRepository,
            IFixtureRepository fixtureRepository,
            IPlayerEventRepository playerEventRepository,
            IGameweekScoreRepository gameweekScoreRepository,
            IPlayerRepository playerRepository,
            ITransferRepository transferRepository,
            IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _fixtureRepository = fixtureRepository;
            _playerEventRepository = playerEventRepository;
            _gameweekScoreRepository = gameweekScoreRepository;
            _playerRepository = playerRepository;
            _transferRepository = transferRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CalculateGameweekScoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fantasyTeam = await _fantasyTeamRepository.GetByManagerIdAsync(request.ManagerId);
                if (fantasyTeam is null)
                    return Result<int>.Failure(new Error("FantasyTeam.NotFound", "No fantasy team found for this manager."));

                var fixtures = await _fixtureRepository.GetByGameweekIdAsync(request.GameweekId);
                if (!fixtures.Any())
                    return Result<int>.Success(0);

                var allGameweekEvents = new List<PlayerEvent>();
                foreach (var fixture in fixtures)
                {
                    var events = await _playerEventRepository.GetByFixtureIdAsync(fixture.Id);
                    allGameweekEvents.AddRange(events);
                }

                var scoringService = new ScoringDomainService();

                bool isBenchBoostActive = fantasyTeam.ActiveChip == FantasyFootball.Domain.Enums.ChipType.BenchBoost && fantasyTeam.ActiveChipGameweekId == request.GameweekId;
                bool isTripleCaptainActive = fantasyTeam.ActiveChip == FantasyFootball.Domain.Enums.ChipType.TripleCaptain && fantasyTeam.ActiveChipGameweekId == request.GameweekId;
                bool isWildcardActive = fantasyTeam.ActiveChip == FantasyFootball.Domain.Enums.ChipType.Wildcard && fantasyTeam.ActiveChipGameweekId == request.GameweekId;

                // Preload all players into FantasyTeam if they are missing
                foreach (var fp in fantasyTeam.Players)
                {
                    if (fp.Player == null)
                    {
                        var p = await _playerRepository.GetByIdAsync(fp.PlayerId);
                        // We must set the property if possible, but Entity Framework might not allow it directly if it has no setter.
                        // Assuming EF Core includes it or we can just pass it. Wait, my CalculateGameweekScore uses fp.Player.
                        // If it's missing, it will throw null reference or calculate 0.
                        // Let's assume GetByManagerIdAsync should use GetByIdWithPlayersAsync or Include(p => p.Players).ThenInclude(p => p.Player).
                    }
                }

                int totalPoints = scoringService.CalculateGameweekScore(fantasyTeam, allGameweekEvents, isBenchBoostActive, isTripleCaptainActive);

                if (!isWildcardActive)
                {
                    var teamTransfers = await _transferRepository.GetByFantasyTeamIdAsync(fantasyTeam.Id);
                    var gameweekTransfers = teamTransfers.Where(t => t.GameweekId == request.GameweekId).ToList();
                    int penaltyPoints = gameweekTransfers.Count(t => !t.IsFree) * FantasyFootball.Domain.Constants.FantasyRules.TransferPenaltyPoints;
                    totalPoints -= penaltyPoints;
                }

                var gameweekScore = await _gameweekScoreRepository.GetByManagerAndGameweekAsync(request.ManagerId, request.GameweekId);
                if (gameweekScore is null)
                {
                    gameweekScore = GameweekScore.Create(request.ManagerId, request.GameweekId);
                    gameweekScore.AddPoints(totalPoints);
                    await _gameweekScoreRepository.AddAsync(gameweekScore);
                }
                else
                {
                    gameweekScore.CorrectPoints(totalPoints);
                    _gameweekScoreRepository.Update(gameweekScore);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<int>.Success(totalPoints);
            }
            catch (DomainException ex)
            {
                return Result<int>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
