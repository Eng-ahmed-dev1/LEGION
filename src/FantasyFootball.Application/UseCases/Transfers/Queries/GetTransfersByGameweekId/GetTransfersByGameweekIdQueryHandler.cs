namespace FantasyFootball.Application.UseCases.Transfers.Queries.GetTransfersByGameweekId
{
    public class GetTransfersByGameweekIdQueryHandler : IRequestHandler<GetTransfersByGameweekIdQuery, Result<IReadOnlyList<TransferDto>>>
    {
        private readonly ITransferRepository _transferRepository;

        public GetTransfersByGameweekIdQueryHandler(ITransferRepository transferRepository)
        => _transferRepository = transferRepository;

        public async Task<Result<IReadOnlyList<TransferDto>>> Handle(GetTransfersByGameweekIdQuery request, CancellationToken cancellationToken)
        {
            var transfers = await _transferRepository.GetByGameweekIdAsync(request.GameweekId);
            return Result<IReadOnlyList<TransferDto>>.Success(transfers.Adapt<IReadOnlyList<TransferDto>>());
        }
    }
}
