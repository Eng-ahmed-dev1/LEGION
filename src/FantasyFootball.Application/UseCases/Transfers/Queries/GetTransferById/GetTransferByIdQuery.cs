namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransferById
{
    public record GetTransferByIdQuery(Guid Id) : IRequest<Result<TransferDto?>>;
}
