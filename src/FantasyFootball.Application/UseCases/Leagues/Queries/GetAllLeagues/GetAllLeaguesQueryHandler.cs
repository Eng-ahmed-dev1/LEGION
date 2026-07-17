namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetAllLeagues
{
    public class GetAllLeaguesQueryHandler : IRequestHandler<GetAllLeaguesQuery, Result<IReadOnlyList<LeagueDto>>>
    {
        private readonly ILeagueRepository _leagueRepository;

        public GetAllLeaguesQueryHandler(ILeagueRepository leagueRepository)
        => _leagueRepository = leagueRepository;

        public async Task<Result<IReadOnlyList<LeagueDto>>> Handle(GetAllLeaguesQuery request, CancellationToken cancellationToken)
        {
            var leagues = await _leagueRepository.GetAllAsync();
            return Result<IReadOnlyList<LeagueDto>>.Success(leagues.Adapt<IReadOnlyList<LeagueDto>>());
        }
    }
}
