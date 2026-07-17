namespace FantasyFootball.Application.UseCases.Transfers.Commands.UpdateTransfer
{
    public class UpdateTransferCommandHandler : IRequestHandler<UpdateTransferCommand, Result<MediatR.Unit>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransferCommandHandler(ITransferRepository transferRepository, IUnitOfWork unitOfWork)
        {
            _transferRepository = transferRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateTransferCommand request, CancellationToken cancellationToken)
        {
            var transfer = await _transferRepository.GetByIdAsync(request.Id);

            if (transfer is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));


            // Note: Transfer entity has no update methods (immutable by design in DDD).

            _transferRepository.Update(transfer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
