namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMembersByManagerId
{
    public class GetLeagueMembersByManagerIdHandler : IRequestHandler<GetLeagueMembersByManagerIdQuery, Result<IReadOnlyList<LeagueMemberDto>>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;

        public GetLeagueMembersByManagerIdHandler(ILeagueMemberRepository leagueMemberRepository)
        => _leagueMemberRepository = leagueMemberRepository;

        public async Task<Result<IReadOnlyList<LeagueMemberDto>>> Handle(GetLeagueMembersByManagerIdQuery request, CancellationToken cancellationToken)
        {
            var leagueMembers = await _leagueMemberRepository.GetByManagerIdAsync(request.ManagerId);
            return Result<IReadOnlyList<LeagueMemberDto>>.Success(leagueMembers.Adapt<IReadOnlyList<LeagueMemberDto>>());
        }
    }
}
