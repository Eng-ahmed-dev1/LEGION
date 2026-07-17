namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.CreateFantasyTeam
{
    public class CreateFantasyTeamCommandHandler : IRequestHandler<CreateFantasyTeamCommand, Result<Guid>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFantasyTeamCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var fantasyTeam = FantasyTeam.Create(request.Name, request.ManagerId);

            await _fantasyTeamRepository.AddAsync(fantasyTeam);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(fantasyTeam.Id);
        }
    }
}
