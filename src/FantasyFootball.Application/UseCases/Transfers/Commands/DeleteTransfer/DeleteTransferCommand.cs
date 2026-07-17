namespace FantasyFootball.Application.UseCases.Transfers.Commands.DeleteTransfer
{
    public record DeleteTransferCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
