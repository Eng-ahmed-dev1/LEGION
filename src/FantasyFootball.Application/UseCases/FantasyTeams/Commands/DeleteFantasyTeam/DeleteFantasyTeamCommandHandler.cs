namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.DeleteFantasyTeam
{
    public class DeleteFantasyTeamCommandHandler : IRequestHandler<DeleteFantasyTeamCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFantasyTeamCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByIdAsync(request.Id);

            if (fantasyTeam is null)
                return Result<Unit>.Failure(new Error("Not Found", "Entity not found."));
            _fantasyTeamRepository.Delete(fantasyTeam);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
