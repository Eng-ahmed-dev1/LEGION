namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.DeleteLeagueMember
{
    public record DeleteLeagueMemberCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
