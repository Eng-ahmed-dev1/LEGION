namespace FantasyFootball.Application.UseCases.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<Guid>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamCommandHandler(ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var team = Team.Create(request.Name, request.ShortName);

                await _teamRepository.AddAsync(team);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(team.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
