namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMembersByManagerId
{
    public record GetLeagueMembersByManagerIdQuery(Guid ManagerId) : IRequest<Result<IReadOnlyList<LeagueMemberDto>>>;
}
