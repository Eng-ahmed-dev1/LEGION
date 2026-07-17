namespace FantasyFootball.Application.UseCases.Transfers.Commands.DeleteTransfer
{
    public class DeleteTransferCommandHandler : IRequestHandler<DeleteTransferCommand, Result<MediatR.Unit>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransferCommandHandler(ITransferRepository transferRepository, IUnitOfWork unitOfWork)
        {
            _transferRepository = transferRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteTransferCommand request, CancellationToken cancellationToken)
        {
            var transfer = await _transferRepository.GetByIdAsync(request.Id);

            if (transfer is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _transferRepository.Delete(transfer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
