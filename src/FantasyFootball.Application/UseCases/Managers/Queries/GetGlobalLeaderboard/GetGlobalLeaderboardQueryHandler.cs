namespace FantasyFootball.Application.UseCases.Managers.Queries.GetGlobalLeaderboard
{
    public class GetGlobalLeaderboardQueryHandler : IRequestHandler<GetGlobalLeaderboardQuery, Result<IReadOnlyList<GlobalLeaderboardDto>>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly ICacheService _cacheService;

        public GetGlobalLeaderboardQueryHandler(
            IManagerRepository managerRepository, 
            IGameweekScoreRepository gameweekScoreRepository,
            ICacheService cacheService)
        {
            _managerRepository = managerRepository;
            _gameweekScoreRepository = gameweekScoreRepository;
            _cacheService = cacheService;
        }

        public async Task<Result<IReadOnlyList<GlobalLeaderboardDto>>> Handle(GetGlobalLeaderboardQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "GlobalLeaderboard";
            var cachedLeaderboard = await _cacheService.GetAsync<IReadOnlyList<GlobalLeaderboardDto>>(cacheKey, cancellationToken);
            
            if (cachedLeaderboard != null)
            {
                return Result<IReadOnlyList<GlobalLeaderboardDto>>.Success(cachedLeaderboard);
            }

            var managers = await _managerRepository.GetAllAsync();
            var scores = await _gameweekScoreRepository.GetAllAsync();

            var leaderboard = managers.Select(m =>
            {
                var totalPoints = scores.Where(s => s.ManagerId == m.Id).Sum(s => s.Points);
                return new GlobalLeaderboardDto(
                    ManagerId: m.Id,
                    TeamName: m.TeamName,
                    UserName: m.UserName,
                    TotalPoints: totalPoints,
                    OverallRank: m.OverallRank
                );
            })
            .OrderByDescending(x => x.TotalPoints)
            .ToList();

            for (int i = 0; i < leaderboard.Count; i++)
            {
                leaderboard[i] = leaderboard[i] with { OverallRank = i + 1 };
            }

            var finalLeaderboard = leaderboard.AsReadOnly();
            
            // Cache the result for 5 minutes to reduce database load
            await _cacheService.SetAsync(cacheKey, finalLeaderboard, TimeSpan.FromMinutes(5), cancellationToken);

            return Result<IReadOnlyList<GlobalLeaderboardDto>>.Success(finalLeaderboard);
        }
    }
}
