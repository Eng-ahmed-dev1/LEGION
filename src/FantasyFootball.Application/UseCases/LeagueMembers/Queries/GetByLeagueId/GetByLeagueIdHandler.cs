namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetByLeagueId
{
    public class GetByLeagueIdHandler : IRequestHandler<GetByLeagueIdQuery, Result<IReadOnlyList<LeagueMemberDto>>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;

        public GetByLeagueIdHandler(ILeagueMemberRepository leagueMemberRepository)
        => _leagueMemberRepository = leagueMemberRepository;

        public async Task<Result<IReadOnlyList<LeagueMemberDto>>> Handle(GetByLeagueIdQuery request, CancellationToken cancellationToken)
        {
            var leagueMembers = await _leagueMemberRepository.GetByLeagueIdAsync(request.LeagueId);
            var sortedMembers = leagueMembers.OrderByDescending(m => m.TotalPoints).ToList();
            return Result<IReadOnlyList<LeagueMemberDto>>.Success(sortedMembers.Adapt<IReadOnlyList<LeagueMemberDto>>());
        }
    }
}
