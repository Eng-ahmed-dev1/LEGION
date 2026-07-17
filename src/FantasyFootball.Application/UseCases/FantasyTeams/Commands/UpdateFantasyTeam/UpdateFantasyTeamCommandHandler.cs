namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.UpdateFantasyTeam
{
    public class UpdateFantasyTeamCommandHandler : IRequestHandler<UpdateFantasyTeamCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFantasyTeamCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByIdAsync(request.Id);

            if (fantasyTeam is null)
                return Result<Unit>.Failure(new Error("Not Found", "This fantasy team not found"));

            fantasyTeam.Rename(request.Name);
            // Note: Budget and FreeTransfers are domain logic restricted and usually updated via specific domain actions like transfers, not directly.

            _fantasyTeamRepository.Update(fantasyTeam);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
