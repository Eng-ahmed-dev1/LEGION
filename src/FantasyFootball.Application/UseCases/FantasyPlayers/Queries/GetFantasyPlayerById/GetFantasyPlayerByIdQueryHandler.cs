namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetFantasyPlayerById
{
    public class GetFantasyPlayerByIdQueryHandler : IRequestHandler<GetFantasyPlayerByIdQuery, Result<FantasyPlayerDto?>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;

        public GetFantasyPlayerByIdQueryHandler(IFantasyPlayerRepository fantasyPlayerRepository)
        => _fantasyPlayerRepository = fantasyPlayerRepository;

        public async Task<Result<FantasyPlayerDto?>> Handle(GetFantasyPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = await _fantasyPlayerRepository.GetByIdAsync(request.Id);
            return Result<FantasyPlayerDto?>.Success(fantasyPlayer.Adapt<FantasyPlayerDto?>());
        }
    }
}
