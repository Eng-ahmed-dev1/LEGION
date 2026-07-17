namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeaguesByManagerId
{
    public record GetLeaguesByManagerIdQuery(Guid ManagerId) : IRequest<Result<IReadOnlyList<LeagueDto>>>;
}
