namespace FantasyFootball.Application.UseCases.Players.Queries.GetByPosition
{
    public class GetByPositionQueryHandler : IRequestHandler<GetByPositionQuery, Result<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetByPositionQueryHandler(IPlayerRepository playerRepository)
        => _playerRepository = playerRepository;

        public async Task<Result<IReadOnlyList<PlayerDto>>> Handle(GetByPositionQuery request, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetByPositionAsync(request.Position);
            return Result<IReadOnlyList<PlayerDto>>.Success(players.Adapt<IReadOnlyList<PlayerDto>>());
        }
    }
}
