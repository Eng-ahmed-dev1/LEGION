namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.UpdateGameweekScore
{
    public class UpdateGameweekScoreCommandHandler : IRequestHandler<UpdateGameweekScoreCommand, Result<MediatR.Unit>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGameweekScoreCommandHandler(IGameweekScoreRepository gameweekScoreRepository, IUnitOfWork unitOfWork)
        {
            _gameweekScoreRepository = gameweekScoreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateGameweekScoreCommand request, CancellationToken cancellationToken)
        {
            var gameweekScore = await _gameweekScoreRepository.GetByIdAsync(request.Id);

            if (gameweekScore is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            try
            {
                gameweekScore.CorrectPoints(request.Points);

                _gameweekScoreRepository.Update(gameweekScore);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<MediatR.Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
