namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.AssignCaptain
{
    public class AssignCaptainCommandHandler : IRequestHandler<AssignCaptainCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignCaptainCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AssignCaptainCommand request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByIdWithPlayersAsync(request.FantasyTeamId);

            if (fantasyTeam is null)
                return Result<Unit>.Failure(new Error("Not Found", "Fantasy Team not found"));

            try
            {
                fantasyTeam.SetCaptain(request.PlayerId);
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
