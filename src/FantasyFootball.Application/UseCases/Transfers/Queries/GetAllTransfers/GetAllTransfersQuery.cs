namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetAllTransfers
{
    public record GetAllTransfersQuery : IRequest<Result<IReadOnlyList<TransferDto>>>;
}
