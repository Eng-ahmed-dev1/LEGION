namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventsByFixtureId
{
    public class GetPlayerEventsByFixtureIdQueryHandler : IRequestHandler<GetPlayerEventsByFixtureIdQuery, Result<IReadOnlyList<PlayerEventDto>>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;

        public GetPlayerEventsByFixtureIdQueryHandler(IPlayerEventRepository playerEventRepository)
        => _playerEventRepository = playerEventRepository;

        public async Task<Result<IReadOnlyList<PlayerEventDto>>> Handle(GetPlayerEventsByFixtureIdQuery request, CancellationToken cancellationToken)
        {
            var playerEvents = await _playerEventRepository.GetByFixtureIdAsync(request.FixtureId);
            return Result<IReadOnlyList<PlayerEventDto>>.Success(playerEvents.Adapt<IReadOnlyList<PlayerEventDto>>());
        }
    }
}
