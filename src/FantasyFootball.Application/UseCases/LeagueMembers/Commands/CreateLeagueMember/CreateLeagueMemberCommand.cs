namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.CreateLeagueMember
{
    public record CreateLeagueMemberCommand(Guid LeagueId, Guid ManagerId) : IRequest<Result<Guid>>;
}
