namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetAllGameweeks
{
    public class GetAllGameweeksQueryHandler : IRequestHandler<GetAllGameweeksQuery, Result<IReadOnlyList<GameweekDto>>>
    {
        private readonly IGameweekRepository _gameweekRepository;

        public GetAllGameweeksQueryHandler(IGameweekRepository gameweekRepository)
        => _gameweekRepository = gameweekRepository;

        public async Task<Result<IReadOnlyList<GameweekDto>>> Handle(GetAllGameweeksQuery request, CancellationToken cancellationToken)
        {
            var gameweeks = await _gameweekRepository.GetAllAsync();
            return Result<IReadOnlyList<GameweekDto>>.Success(gameweeks.Adapt<IReadOnlyList<GameweekDto>>());
        }
    }
}
