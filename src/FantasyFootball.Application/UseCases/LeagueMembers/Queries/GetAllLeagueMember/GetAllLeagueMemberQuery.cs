namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetAllLeagueMembers
{
    public record GetAllLeagueMemberQuery : IRequest<Result<IReadOnlyList<LeagueMemberDto>>>;
}
