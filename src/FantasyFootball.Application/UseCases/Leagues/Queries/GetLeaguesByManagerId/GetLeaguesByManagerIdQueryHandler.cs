namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeaguesByManagerId
{
    public class GetLeaguesByManagerIdQueryHandler : IRequestHandler<GetLeaguesByManagerIdQuery, Result<IReadOnlyList<LeagueDto>>>
    {
        private readonly ILeagueRepository _leagueRepository;

        public GetLeaguesByManagerIdQueryHandler(ILeagueRepository leagueRepository)
        => _leagueRepository = leagueRepository;

        public async Task<Result<IReadOnlyList<LeagueDto>>> Handle(GetLeaguesByManagerIdQuery request, CancellationToken cancellationToken)
        {
            var leagues = await _leagueRepository.GetByManagerIdAsync(request.ManagerId);
            return Result<IReadOnlyList<LeagueDto>>.Success(leagues.Adapt<IReadOnlyList<LeagueDto>>());
        }
    }
}
