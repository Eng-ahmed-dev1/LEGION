namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeagueById
{
    public class GetLeagueByIdQueryHandler : IRequestHandler<GetLeagueByIdQuery, Result<LeagueDto?>>
    {
        private readonly ILeagueRepository _leagueRepository;

        public GetLeagueByIdQueryHandler(ILeagueRepository leagueRepository)
        => _leagueRepository = leagueRepository;

        public async Task<Result<LeagueDto?>> Handle(GetLeagueByIdQuery request, CancellationToken cancellationToken)
        {
            var league = await _leagueRepository.GetByIdAsync(request.Id);
            return Result<LeagueDto?>.Success(league.Adapt<LeagueDto?>());
        }
    }
}
