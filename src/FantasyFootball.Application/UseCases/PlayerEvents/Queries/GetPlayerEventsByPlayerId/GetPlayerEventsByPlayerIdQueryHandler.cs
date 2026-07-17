namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventsByPlayerId
{
    public class GetPlayerEventsByPlayerIdQueryHandler : IRequestHandler<GetPlayerEventsByPlayerIdQuery, Result<IReadOnlyList<PlayerEventDto>>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;

        public GetPlayerEventsByPlayerIdQueryHandler(IPlayerEventRepository playerEventRepository)
        => _playerEventRepository = playerEventRepository;

        public async Task<Result<IReadOnlyList<PlayerEventDto>>> Handle(GetPlayerEventsByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var playerEvents = await _playerEventRepository.GetByPlayerIdAsync(request.PlayerId);
            return Result<IReadOnlyList<PlayerEventDto>>.Success(playerEvents.Adapt<IReadOnlyList<PlayerEventDto>>());
        }
    }
}
