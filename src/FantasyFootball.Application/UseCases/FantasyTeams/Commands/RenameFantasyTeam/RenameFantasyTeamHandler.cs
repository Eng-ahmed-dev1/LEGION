namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RenameFantasyTeam
{
    public class RenameFantasyTeamHandler : IRequestHandler<RenameFantasyTeamCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RenameFantasyTeamHandler(IFantasyTeamRepository fantasyTeam, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeam;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Unit>> Handle(RenameFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByIdAsync(request.id);

            if (fantasyTeam is null)
                return Result<Unit>.Failure(new Error("FantasyTeam.NotFound", "FantasyTeam not found"));

            fantasyTeam.Rename(request.name);
            _fantasyTeamRepository.Update(fantasyTeam);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}