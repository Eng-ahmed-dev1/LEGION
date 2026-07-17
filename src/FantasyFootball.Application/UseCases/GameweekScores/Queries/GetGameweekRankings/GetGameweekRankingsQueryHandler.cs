namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekRankings
{
    public class GetGameweekRankingsQueryHandler : IRequestHandler<GetGameweekRankingsQuery, Result<IReadOnlyList<GameweekScoreDto>>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;

        public GetGameweekRankingsQueryHandler(IGameweekScoreRepository gameweekScoreRepository)
        {
            _gameweekScoreRepository = gameweekScoreRepository;
        }

        public async Task<Result<IReadOnlyList<GameweekScoreDto>>> Handle(GetGameweekRankingsQuery request, CancellationToken cancellationToken)
        {
            var gameweekScores = await _gameweekScoreRepository.GetAllAsync();
            var rankings = gameweekScores
                .Where(x => x.GameweekId == request.GameweekId)
                .OrderByDescending(x => x.Points)
                .ToList();

            return Result<IReadOnlyList<GameweekScoreDto>>.Success(rankings.Adapt<IReadOnlyList<GameweekScoreDto>>());
        }
    }
}
