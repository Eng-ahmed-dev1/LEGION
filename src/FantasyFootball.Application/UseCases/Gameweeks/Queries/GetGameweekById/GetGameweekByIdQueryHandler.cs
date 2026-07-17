namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetGameweekById
{
    public class GetGameweekByIdQueryHandler : IRequestHandler<GetGameweekByIdQuery, Result<GameweekDto?>>
    {
        private readonly IGameweekRepository _gameweekRepository;

        public GetGameweekByIdQueryHandler(IGameweekRepository gameweekRepository)
        => _gameweekRepository = gameweekRepository;

        public async Task<Result<GameweekDto?>> Handle(GetGameweekByIdQuery request, CancellationToken cancellationToken)
        {
            var gameweek = await _gameweekRepository.GetByIdAsync(request.Id);
            return Result<GameweekDto?>.Success(gameweek.Adapt<GameweekDto?>());
        }
    }
}
