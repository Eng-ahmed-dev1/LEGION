namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.ToggleAutoSub
{
    public class ToggleAutoSubCommandHandler : IRequestHandler<ToggleAutoSubCommand, Result<Guid>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleAutoSubCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ToggleAutoSubCommand request, CancellationToken cancellationToken)
        {
            var team = await _fantasyTeamRepository.GetByIdAsync(request.FantasyTeamId);
            if (team == null) return Result<Guid>.Failure(new Error("FantasyTeamNotFound", "Fantasy Team not found."));

            team.ToggleAutoSub();
            _fantasyTeamRepository.Update(team);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result<Guid>.Success(team.Id);
        }
    }
}
