namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransfersByFantasyTeamId
{
    public record GetTransfersByFantasyTeamIdQuery(Guid FantasyTeamId) : IRequest<Result<IReadOnlyList<TransferDto>>>;
}
