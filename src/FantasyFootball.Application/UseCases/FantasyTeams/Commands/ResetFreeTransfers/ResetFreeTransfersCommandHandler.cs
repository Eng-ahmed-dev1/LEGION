namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.ResetFreeTransfers
{
    public class ResetFreeTransfersCommandHandler : IRequestHandler<ResetFreeTransfersCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResetFreeTransfersCommandHandler(
            IFantasyTeamRepository fantasyTeamRepository,
            IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(ResetFreeTransfersCommand request, CancellationToken cancellationToken)
        {
            var team = await _fantasyTeamRepository.GetByIdAsync(request.FantasyTeamId);
            if (team is null)
                return Result<Unit>.Failure(new Error("Not Found", "Fantasy team not found"));

            try
            {
                team.ResetFreeTransfers();

                _fantasyTeamRepository.Update(team);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain Error", ex.Message));
            }
        }
    }
}
