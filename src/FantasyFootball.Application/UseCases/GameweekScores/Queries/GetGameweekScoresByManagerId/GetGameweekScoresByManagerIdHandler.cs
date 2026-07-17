namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoresByManagerId
{
    public class GetGameweekScoresByManagerIdHandler : IRequestHandler<GetGameweekScoresByManagerIdQuery, Result<GameweekScoreDto?>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;

        public GetGameweekScoresByManagerIdHandler(IGameweekScoreRepository gameweekScoreRepository)
        => _gameweekScoreRepository = gameweekScoreRepository;

        public async Task<Result<GameweekScoreDto?>> Handle(GetGameweekScoresByManagerIdQuery request, CancellationToken cancellationToken)
        {
            var gameweekScore = await _gameweekScoreRepository.GetByManagerIdAsync(request.ManagerId);
            return Result<GameweekScoreDto?>.Success(gameweekScore.Adapt<GameweekScoreDto?>());
        }
    }
}
