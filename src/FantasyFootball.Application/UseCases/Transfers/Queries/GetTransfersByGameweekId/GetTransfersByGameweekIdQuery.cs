namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransfersByGameweekId
{
    public record GetTransfersByGameweekIdQuery(Guid GameweekId) : IRequest<Result<IReadOnlyList<TransferDto>>>;
}
