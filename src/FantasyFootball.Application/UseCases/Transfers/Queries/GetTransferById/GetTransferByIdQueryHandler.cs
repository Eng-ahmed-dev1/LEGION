namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransferById
{
    public class GetTransferByIdQueryHandler : IRequestHandler<GetTransferByIdQuery, Result<TransferDto?>>
    {
        private readonly ITransferRepository _transferRepository;

        public GetTransferByIdQueryHandler(ITransferRepository transferRepository)
        => _transferRepository = transferRepository;

        public async Task<Result<TransferDto?>> Handle(GetTransferByIdQuery request, CancellationToken cancellationToken)
        {
            var transfer = await _transferRepository.GetByIdAsync(request.Id);
            return Result<TransferDto?>.Success(transfer.Adapt<TransferDto?>());
        }
    }
}
