namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetByLeagueId
{
    public record GetByLeagueIdQuery(Guid LeagueId) : IRequest<Result<IReadOnlyList<LeagueMemberDto>>>;
}
