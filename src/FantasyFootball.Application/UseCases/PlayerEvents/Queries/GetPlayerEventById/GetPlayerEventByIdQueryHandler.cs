namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventById
{
    public class GetPlayerEventByIdQueryHandler : IRequestHandler<GetPlayerEventByIdQuery, Result<PlayerEventDto?>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;

        public GetPlayerEventByIdQueryHandler(IPlayerEventRepository playerEventRepository)
        => _playerEventRepository = playerEventRepository;

        public async Task<Result<PlayerEventDto?>> Handle(GetPlayerEventByIdQuery request, CancellationToken cancellationToken)
        {
            var playerEvent = await _playerEventRepository.GetByIdAsync(request.Id);
            return Result<PlayerEventDto?>.Success(playerEvent.Adapt<PlayerEventDto?>());
        }
    }
}
