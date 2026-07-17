namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetActiveGameweek
{
    public class GetActiveGameweekHandler : IRequestHandler<GetActiveGameweekQuery, Result<GameweekDto?>>
    {
        private readonly IGameweekRepository _gameweekRepository;

        public GetActiveGameweekHandler(IGameweekRepository gameweekRepository)
        => _gameweekRepository = gameweekRepository;

        public async Task<Result<GameweekDto?>> Handle(GetActiveGameweekQuery request, CancellationToken cancellationToken)
        {
            var gameweek = await _gameweekRepository.GetActiveGameweekAsync();
            return Result<GameweekDto?>.Success(gameweek.Adapt<GameweekDto?>());
        }
    }
}
