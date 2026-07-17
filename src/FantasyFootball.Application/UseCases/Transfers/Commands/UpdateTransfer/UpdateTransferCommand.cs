namespace FantasyFootball.Application.UseCases.Transfers.Commands.UpdateTransfer
{
    public record UpdateTransferCommand(
        Guid Id,
        Guid FantasyTeamId,
        Guid PlayerInId,
        Guid PlayerOutId,
        Guid GameweekId,
        bool IsFree) : IRequest<Result<MediatR.Unit>>;
}
