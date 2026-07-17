namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeagueLeaderboard
{
    public record GetLeagueLeaderboardQuery(Guid LeagueId) : IRequest<Result<List<LeagueLeaderboardEntryDto>>>;

    public record LeagueLeaderboardEntryDto(Guid ManagerId, string ManagerName, string TeamName, int TotalPoints, int Rank);
}
