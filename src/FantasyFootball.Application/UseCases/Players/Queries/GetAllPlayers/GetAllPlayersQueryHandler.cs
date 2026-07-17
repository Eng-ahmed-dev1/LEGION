namespace FantasyFootball.Application.UseCases.Players.Queries.GetAllPlayers
{
    public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, Result<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository;
        public GetAllPlayersQueryHandler(IPlayerRepository playerRepository) => _playerRepository = playerRepository;

        public async Task<Result<IReadOnlyList<PlayerDto>>> Handle(
      GetAllPlayersQuery request,
      CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetFilteredPlayersAsync(request.Parameters);
            return Result<IReadOnlyList<PlayerDto>>.Success(players.Adapt<IReadOnlyList<PlayerDto>>());
        }
    }
}
