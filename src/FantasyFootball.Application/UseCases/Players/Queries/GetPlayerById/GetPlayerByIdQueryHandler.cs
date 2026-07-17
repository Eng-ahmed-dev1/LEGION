namespace FantasyFootball.Application.UseCases.Players.Queries.GetPlayerById
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Result<PlayerDto>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository)
            => _playerRepository = playerRepository;

        public async Task<Result<PlayerDto>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id);

            if (player is null)
                return Result<PlayerDto>.Failure(new Error("Player.NotFound", "Player not found"));

            return Result<PlayerDto>.Success(player.Adapt<PlayerDto>());
        }
    }
}