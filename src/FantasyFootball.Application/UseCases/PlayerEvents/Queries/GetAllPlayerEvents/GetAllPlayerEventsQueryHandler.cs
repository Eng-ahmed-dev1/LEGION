namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetAllPlayerEvents
{
    public class GetAllPlayerEventsQueryHandler : IRequestHandler<GetAllPlayerEventsQuery, Result<IReadOnlyList<PlayerEventDto>>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;

        public GetAllPlayerEventsQueryHandler(IPlayerEventRepository playerEventRepository)
        => _playerEventRepository = playerEventRepository;

        public async Task<Result<IReadOnlyList<PlayerEventDto>>> Handle(GetAllPlayerEventsQuery request, CancellationToken cancellationToken)
        {
            var playerEvents = await _playerEventRepository.GetAllAsync();
            return Result<IReadOnlyList<PlayerEventDto>>.Success(playerEvents.Adapt<IReadOnlyList<PlayerEventDto>>());
        }
    }
}
