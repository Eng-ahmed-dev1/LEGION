namespace FantasyFootball.Application.UseCases.Players.Queries.GetByTeamId
{
    public class GetByTeamIdHandler : IRequestHandler<GetByTeamIdQuery, Result<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository;
        public GetByTeamIdHandler(IPlayerRepository playerRepository)
        => _playerRepository = playerRepository;

        public async Task<Result<IReadOnlyList<PlayerDto>>> Handle(GetByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetByTeamIdAsync(request.TeamId);
            return Result<IReadOnlyList<PlayerDto>>.Success(players.Adapt<IReadOnlyList<PlayerDto>>());
        }
    }
}
