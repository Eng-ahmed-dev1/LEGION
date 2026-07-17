namespace FantasyFootball.Application.UseCases.Managers.Commands.UpdateManagerRank
{
    public class UpdateManagerRankCommandHandler : IRequestHandler<UpdateManagerRankCommand, Result<Unit>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateManagerRankCommandHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateManagerRankCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.ManagerId);

            if (manager is null)
                return Result<Unit>.Failure(new Error("Not.Found", "Manager not found"));

            try
            {
                manager.UpdateRank(request.NewRank);

                _managerRepository.Update(manager);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
