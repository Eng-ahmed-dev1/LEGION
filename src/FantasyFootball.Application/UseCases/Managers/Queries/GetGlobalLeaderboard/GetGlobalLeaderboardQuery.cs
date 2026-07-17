namespace FantasyFootball.Application.UseCases.Managers.Queries.GetGlobalLeaderboard
{
    public record GetGlobalLeaderboardQuery() : IRequest<Result<IReadOnlyList<GlobalLeaderboardDto>>>;
}
