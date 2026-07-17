namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetAllLeagueMembers
{
    public class GetAllLeagueMemberHandler : IRequestHandler<GetAllLeagueMemberQuery, Result<IReadOnlyList<LeagueMemberDto>>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;

        public GetAllLeagueMemberHandler(ILeagueMemberRepository leagueMemberRepository)
        => _leagueMemberRepository = leagueMemberRepository;

        public async Task<Result<IReadOnlyList<LeagueMemberDto>>> Handle(GetAllLeagueMemberQuery request, CancellationToken cancellationToken)
        {
            var leagueMembers = await _leagueMemberRepository.GetAllAsync();
            return Result<IReadOnlyList<LeagueMemberDto>>.Success(leagueMembers.Adapt<IReadOnlyList<LeagueMemberDto>>());
        }
    }
}
