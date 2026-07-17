namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetAllTransfers
{
    public class GetAllTransfersQueryHandler : IRequestHandler<GetAllTransfersQuery, Result<IReadOnlyList<TransferDto>>>
    {
        private readonly ITransferRepository _transferRepository;

        public GetAllTransfersQueryHandler(ITransferRepository transferRepository)
        => _transferRepository = transferRepository;

        public async Task<Result<IReadOnlyList<TransferDto>>> Handle(GetAllTransfersQuery request, CancellationToken cancellationToken)
        {
            var transfers = await _transferRepository.GetAllAsync();
            return Result<IReadOnlyList<TransferDto>>.Success(transfers.Adapt<IReadOnlyList<TransferDto>>());
        }
    }
}
