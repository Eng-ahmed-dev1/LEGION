namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetByFantasyTeamId
{
    public class GetByFantasyTeamIdHandler : IRequestHandler<GetByFantasyTeamIdQuery, Result<FantasyPlayerDto?>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;

        public GetByFantasyTeamIdHandler(IFantasyPlayerRepository fantasyPlayerRepository)
        => _fantasyPlayerRepository = fantasyPlayerRepository;

        public async Task<Result<FantasyPlayerDto?>> Handle(GetByFantasyTeamIdQuery request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = await _fantasyPlayerRepository.GetByFantasyTeamIdAsync(request.Id);
            return Result<FantasyPlayerDto?>.Success(fantasyPlayer.Adapt<FantasyPlayerDto?>());
        }
    }
}
