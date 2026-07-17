namespace FantasyFootball.Application.UseCases.PlayerEvents.Events
{
    public class LiveScoreUpdateEventHandler : INotificationHandler<LiveScoreUpdateEvent>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IMatchNotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public LiveScoreUpdateEventHandler(
            IFantasyTeamRepository fantasyTeamRepository,
            IFixtureRepository fixtureRepository,
            IGameweekScoreRepository gameweekScoreRepository,
            IManagerRepository managerRepository,
            IMatchNotificationService notificationService,
            IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _fixtureRepository = fixtureRepository;
            _gameweekScoreRepository = gameweekScoreRepository;
            _managerRepository = managerRepository;
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LiveScoreUpdateEvent notification, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureRepository.GetByIdAsync(notification.FixtureId);
            if (fixture == null) return;

            var gameweekId = fixture.GameweekId;

            var allTeams = await _fantasyTeamRepository.GetAllAsync();

            var teamsWithPlayer = allTeams.Where(t => 
                t.Players.Any(p => p.PlayerId == notification.PlayerId && !p.IsOnBench)
            ).ToList();

            foreach (var team in teamsWithPlayer)
            {
                var fantasyPlayer = team.Players.First(p => p.PlayerId == notification.PlayerId);
                int pointsToAdd = notification.PointsGained;

                if (fantasyPlayer.IsCaptain)
                {
                    bool isTriple = team.ActiveChip == FantasyFootball.Domain.Enums.ChipType.TripleCaptain 
                                 && team.ActiveChipGameweekId == gameweekId;
                    pointsToAdd *= isTriple ? 3 : 2;
                }

                var gameweekScore = await _gameweekScoreRepository.GetByManagerAndGameweekAsync(team.ManagerId, gameweekId);
                
                if (gameweekScore == null)
                {
                    gameweekScore = GameweekScore.Create(team.ManagerId, gameweekId);
                    gameweekScore.AddPoints(pointsToAdd);
                    await _gameweekScoreRepository.AddAsync(gameweekScore);
                }
                else
                {
                    gameweekScore.CorrectPoints(gameweekScore.Points + pointsToAdd);
                    _gameweekScoreRepository.Update(gameweekScore);
                }
            }

            // 6. Recalculate global ranks and notify users who improved
            var allManagers = await _managerRepository.GetAllAsync();
            var allScores = await _gameweekScoreRepository.GetAllAsync();

            var leaderboard = allManagers.Select(m => new
            {
                Manager = m,
                TotalPoints = allScores.Where(s => s.ManagerId == m.Id).Sum(s => s.Points)
            })
            .OrderByDescending(x => x.TotalPoints)  
            .ToList();

            for (int i = 0; i < leaderboard.Count; i++)
            {
                var entry = leaderboard[i];
                int newRank = i + 1;
                var manager = entry.Manager;
                int oldRank = manager.OverallRank;

                // Send notification only if rank improved (smaller number is better)
                if (oldRank != 0 && newRank < oldRank)
                {
                    string message = $"🎉 Legend! You've climbed the ranks! You are now #{newRank} globally with {entry.TotalPoints} points! 🏆";
                    await _notificationService.SendPersonalNotificationAsync(manager.ApplicationUserId.ToString(), message);
                }

                if (oldRank != newRank)
                {
                    manager.UpdateRank(newRank);
                    _managerRepository.Update(manager);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
