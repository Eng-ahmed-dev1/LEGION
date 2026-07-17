namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMemberById
{
    public record GetLeagueMemberByIdQuery(Guid Id) : IRequest<Result<LeagueMemberDto?>>;
}
