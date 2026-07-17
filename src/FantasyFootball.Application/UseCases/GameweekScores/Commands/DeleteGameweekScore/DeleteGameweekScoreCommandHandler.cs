namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.DeleteGameweekScore
{
    public class DeleteGameweekScoreCommandHandler : IRequestHandler<DeleteGameweekScoreCommand, Result<MediatR.Unit>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGameweekScoreCommandHandler(IGameweekScoreRepository gameweekScoreRepository, IUnitOfWork unitOfWork)
        {
            _gameweekScoreRepository = gameweekScoreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteGameweekScoreCommand request, CancellationToken cancellationToken)
        {
            var score = await _gameweekScoreRepository.GetByIdAsync(request.Id);

            if (score is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _gameweekScoreRepository.Delete(score);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
