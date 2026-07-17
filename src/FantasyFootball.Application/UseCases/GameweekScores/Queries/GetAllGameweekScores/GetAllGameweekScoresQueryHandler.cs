namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetAllGameweekScores
{
    public class GetAllGameweekScoresQueryHandler : IRequestHandler<GetAllGameweekScoresQuery, Result<IReadOnlyList<GameweekScoreDto>>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;

        public GetAllGameweekScoresQueryHandler(IGameweekScoreRepository gameweekScoreRepository)
        => _gameweekScoreRepository = gameweekScoreRepository;

        public async Task<Result<IReadOnlyList<GameweekScoreDto>>> Handle(GetAllGameweekScoresQuery request, CancellationToken cancellationToken)
        {
            var scores = await _gameweekScoreRepository.GetAllAsync();
            return Result<IReadOnlyList<GameweekScoreDto>>.Success(scores.Adapt<IReadOnlyList<GameweekScoreDto>>());
        }
    }
}
