namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.ResetFreeTransfers
{
    public record ResetFreeTransfersCommand(Guid FantasyTeamId) : IRequest<Result<Unit>>;
}
