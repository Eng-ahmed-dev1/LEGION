namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.AssignViceCaptain
{
    public class AssignViceCaptainHandler : IRequestHandler<AssignViceCaptainCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AssignViceCaptainHandler(IFantasyTeamRepository fantasyTeamRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _fantasyTeamRepository = fantasyTeamRepository;
        }
        public async Task<Result<Unit>> Handle(AssignViceCaptainCommand request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByIdWithPlayersAsync(request.FantasyTeamId);
            if (fantasyTeam is null)
                return Result<Unit>.Failure(new Error("Not Found", "Fantasy Team not found"));

            try
            {
                fantasyTeam.SetViceCaptain(request.PlayerId);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("DomainError", ex.Message));
            }
            _fantasyTeamRepository.Update(fantasyTeam);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}