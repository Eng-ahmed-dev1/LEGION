namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.UpdateLeagueMember
{
    public record UpdateLeagueMemberCommand(
        Guid Id,
        Guid LeagueId,
        Guid ManagerId,
        int TotalPoints) : IRequest<Result<MediatR.Unit>>;
}
