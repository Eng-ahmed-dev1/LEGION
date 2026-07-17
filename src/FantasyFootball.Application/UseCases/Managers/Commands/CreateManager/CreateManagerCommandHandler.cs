namespace FantasyFootball.Application.UseCases.Managers.Commands.CreateManager
{
    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, Result<Guid>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateManagerCommandHandler(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var manager = Manager.Create(request.TeamName, request.ApplicationUserId, request.UserName);

                await _managerRepository.AddAsync(manager);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(manager.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
