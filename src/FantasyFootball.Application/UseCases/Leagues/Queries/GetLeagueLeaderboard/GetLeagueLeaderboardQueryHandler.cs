namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeagueLeaderboard
{
    public class GetLeagueLeaderboardQueryHandler : IRequestHandler<GetLeagueLeaderboardQuery, Result<List<LeagueLeaderboardEntryDto>>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IFantasyTeamRepository _fantasyTeamRepository;

        public GetLeagueLeaderboardQueryHandler(
            ILeagueRepository leagueRepository, 
            IGameweekScoreRepository gameweekScoreRepository,
            IFantasyTeamRepository fantasyTeamRepository)
        {
            _leagueRepository = leagueRepository;
            _gameweekScoreRepository = gameweekScoreRepository;
            _fantasyTeamRepository = fantasyTeamRepository;
        }

        public async Task<Result<List<LeagueLeaderboardEntryDto>>> Handle(GetLeagueLeaderboardQuery request, CancellationToken cancellationToken)
        {
            var league = await _leagueRepository.GetByIdAsync(request.LeagueId);
            if (league == null)
                return Result<List<LeagueLeaderboardEntryDto>>.Failure(new Error("NotFound", "League not found"));

            var leaderboard = new List<LeagueLeaderboardEntryDto>();

            var allScores = await _gameweekScoreRepository.GetAllAsync();

            // For simplicity, calculating by fetching each manager's score. 
            // In a real scenario, this would be a specialized DB View or optimized Query.
            foreach (var member in league.Members)
            {
                var team = await _fantasyTeamRepository.GetByManagerIdAsync(member.ManagerId);
                var managerScores = allScores.Where(s => s.ManagerId == member.ManagerId);
                int totalPoints = managerScores.Sum(s => s.Points);

                leaderboard.Add(new LeagueLeaderboardEntryDto(
                    ManagerId: member.ManagerId,
                    ManagerName: member.Manager?.UserName ?? "Unknown",
                    TeamName: team?.Name ?? "Unknown Team",
                    TotalPoints: totalPoints,
                    Rank: 0
                ));
            }

            var sortedLeaderboard = leaderboard
                .OrderByDescending(x => x.TotalPoints)
                .Select((entry, index) => entry with { Rank = index + 1 })
                .ToList();

            return Result<List<LeagueLeaderboardEntryDto>>.Success(sortedLeaderboard);
        }
    }
}
