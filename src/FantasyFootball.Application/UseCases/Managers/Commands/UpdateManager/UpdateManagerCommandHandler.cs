namespace FantasyFootball.Application.UseCases.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, Result<MediatR.Unit>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateManagerCommandHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.Id);

            if (manager is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));


            try
            {
                manager.RenameTeam(request.TeamName);
                manager.ChangeUserName(request.UserName);

                _managerRepository.Update(manager);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<MediatR.Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
