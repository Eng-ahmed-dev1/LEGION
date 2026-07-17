namespace FantasyFootball.Application.UseCases.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Result<MediatR.Unit>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.Id);

            if (team is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _teamRepository.Delete(team);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
