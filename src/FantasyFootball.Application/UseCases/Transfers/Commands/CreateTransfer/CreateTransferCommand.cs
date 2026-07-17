namespace FantasyFootball.Application.UseCases.Transfers.Commands.CreateTransfer
{
    public record CreateTransferCommand(
        Guid FantasyTeamId,
        Guid PlayerInId,
        Guid PlayerOutId,
        Guid GameweekId) : IRequest<Result<Guid>>;
}
