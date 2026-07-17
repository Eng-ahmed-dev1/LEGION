namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.AddPoints
{
    public class AddPointsToGameweekScoreCommandHandler : IRequestHandler<AddPointsToGameweekScoreCommand, Result<Unit>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPointsToGameweekScoreCommandHandler(IGameweekScoreRepository gameweekScoreRepository, IUnitOfWork unitOfWork)
        {
            _gameweekScoreRepository = gameweekScoreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AddPointsToGameweekScoreCommand request, CancellationToken cancellationToken)
        {
            var gameweekScore = await _gameweekScoreRepository.GetByIdAsync(request.GameweekScoreId);

            if (gameweekScore is null)
                return Result<Unit>.Failure(new Error("Not.Found", "GameweekScore not found"));

            try
            {
                gameweekScore.AddPoints(request.Points);

                _gameweekScoreRepository.Update(gameweekScore);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
