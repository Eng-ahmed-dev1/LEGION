namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CreateGameweekScore
{
    public class CreateGameweekScoreCommandHandler : IRequestHandler<CreateGameweekScoreCommand, Result<Guid>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameweekScoreCommandHandler(IGameweekScoreRepository gameweekScoreRepository, IUnitOfWork unitOfWork)
        {
            _gameweekScoreRepository = gameweekScoreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateGameweekScoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var score = GameweekScore.Create(request.ManagerId, request.GameweekId);

                await _gameweekScoreRepository.AddAsync(score);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(score.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
