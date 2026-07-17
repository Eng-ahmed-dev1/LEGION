namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetAllFantasyPlayers
{
    public class GetAllFantasyPlayersQueryHandler : IRequestHandler<GetAllFantasyPlayersQuery, Result<IReadOnlyList<FantasyPlayerDto>>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;

        public GetAllFantasyPlayersQueryHandler(IFantasyPlayerRepository fantasyPlayerRepository)
        => _fantasyPlayerRepository = fantasyPlayerRepository;

        public async Task<Result<IReadOnlyList<FantasyPlayerDto>>> Handle(GetAllFantasyPlayersQuery request, CancellationToken cancellationToken)
        {
            var fantasyPlayers = await _fantasyPlayerRepository.GetAllAsync();
            return Result<IReadOnlyList<FantasyPlayerDto>>.Success(fantasyPlayers.Adapt<IReadOnlyList<FantasyPlayerDto>>());
        }
    }
}
