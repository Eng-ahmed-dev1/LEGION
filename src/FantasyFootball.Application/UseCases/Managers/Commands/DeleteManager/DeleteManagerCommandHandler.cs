namespace FantasyFootball.Application.UseCases.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, Result<MediatR.Unit>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteManagerCommandHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.Id);

            if (manager is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _managerRepository.Delete(manager);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
