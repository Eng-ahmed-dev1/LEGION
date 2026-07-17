namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.UseTransfer
{
    public record UseTransferCommand(Guid FantasyTeamId) : IRequest<Result<Unit>>;
}
