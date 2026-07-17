namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoreById
{
    public class GetGameweekScoreByIdQueryHandler : IRequestHandler<GetGameweekScoreByIdQuery, Result<GameweekScoreDto?>>
    {
        private readonly IGameweekScoreRepository _gameweekScoreRepository;

        public GetGameweekScoreByIdQueryHandler(IGameweekScoreRepository gameweekScoreRepository)
        => _gameweekScoreRepository = gameweekScoreRepository;

        public async Task<Result<GameweekScoreDto?>> Handle(GetGameweekScoreByIdQuery request, CancellationToken cancellationToken)
        {
            var score = await _gameweekScoreRepository.GetByIdAsync(request.Id);
            return Result<GameweekScoreDto?>.Success(score.Adapt<GameweekScoreDto?>());
        }
    }
}
