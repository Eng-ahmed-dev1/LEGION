namespace FantasyFootball.Application.UseCases.Leagues.Commands.CreateLeague
{
    public class CreateLeagueCommandHandler : IRequestHandler<CreateLeagueCommand, Result<Guid>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeagueCommandHandler(
            ILeagueRepository leagueRepository, 
            IManagerRepository managerRepository,
            IUnitOfWork unitOfWork)
        {
            _leagueRepository = leagueRepository;
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var manager = await _managerRepository.GetByIdAsync(request.CreatedById);
                if (manager == null)
                    return Result<Guid>.Failure(new Error("Manager.NotFound", "Manager not found."));

                var allLeagues = await _leagueRepository.GetAllAsync();
                int createdLeaguesCount = allLeagues.Count(l => l.CreatedById == request.CreatedById);

                if (createdLeaguesCount >= 2 && !manager.IsPremium)
                {
                    return Result<Guid>.Failure(new Error(
                        "League.LimitExceeded", 
                        "Free users can only create up to 2 leagues. Upgrade to Premium to create more."
                    ));
                }

                var league = League.Create(request.Name, request.type, request.CreatedById);

                await _leagueRepository.AddAsync(league);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(league.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
