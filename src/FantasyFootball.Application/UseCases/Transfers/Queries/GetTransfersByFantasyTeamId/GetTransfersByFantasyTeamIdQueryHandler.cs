namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransfersByFantasyTeamId
{
    public class GetTransfersByFantasyTeamIdQueryHandler : IRequestHandler<GetTransfersByFantasyTeamIdQuery, Result<IReadOnlyList<TransferDto>>>
    {
        private readonly ITransferRepository _transferRepository;

        public GetTransfersByFantasyTeamIdQueryHandler(ITransferRepository transferRepository)
        => _transferRepository = transferRepository;

        public async Task<Result<IReadOnlyList<TransferDto>>> Handle(GetTransfersByFantasyTeamIdQuery request, CancellationToken cancellationToken)
        {
            var transfers = await _transferRepository.GetByFantasyTeamIdAsync(request.FantasyTeamId);
            return Result<IReadOnlyList<TransferDto>>.Success(transfers.Adapt<IReadOnlyList<TransferDto>>());
        }
    }
}
