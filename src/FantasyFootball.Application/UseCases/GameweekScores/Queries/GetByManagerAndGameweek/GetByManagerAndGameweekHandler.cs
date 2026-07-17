namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetByManagerAndGameweek
{
    public class GetByManagerAndGameweekHandler : IRequestHandler<GetByManagerAndGameweekQuery, Result<GameweekScoreDto?>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;

        public GetByManagerAndGameweekHandler(IGameweekScoreRepository gameweekScoreRepository)
        => _gameweekScoreRepository = gameweekScoreRepository;

        public async Task<Result<GameweekScoreDto?>> Handle(GetByManagerAndGameweekQuery request, CancellationToken cancellationToken)
        {
            var gameweekScore = await _gameweekScoreRepository.GetByManagerAndGameweekAsync(request.ManagerId, request.GameweekId);
            return Result<GameweekScoreDto?>.Success(gameweekScore.Adapt<GameweekScoreDto?>());
        }
    }
}
