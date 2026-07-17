namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.AssignViceCaptain
{
    public record AssignViceCaptainCommand(Guid FantasyTeamId, Guid PlayerId) : IRequest<Result<Unit>>;
}