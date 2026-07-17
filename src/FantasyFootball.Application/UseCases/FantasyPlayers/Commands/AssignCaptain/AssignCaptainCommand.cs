namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.AssignCaptain
{
    public record AssignCaptainCommand(Guid FantasyTeamId, Guid PlayerId) : IRequest<Result<Unit>>;
}
