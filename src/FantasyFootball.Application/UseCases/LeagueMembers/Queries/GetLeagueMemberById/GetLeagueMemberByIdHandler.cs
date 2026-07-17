namespace FantasyFootball.Application.UseCases.LeagueMembers.Queries.GetLeagueMemberById
{
    public class GetLeagueMemberByIdHandler : IRequestHandler<GetLeagueMemberByIdQuery, Result<LeagueMemberDto?>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;

        public GetLeagueMemberByIdHandler(ILeagueMemberRepository leagueMemberRepository)
        => _leagueMemberRepository = leagueMemberRepository;

        public async Task<Result<LeagueMemberDto?>> Handle(GetLeagueMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var leagueMember = await _leagueMemberRepository.GetByIdAsync(request.Id);
            return Result<LeagueMemberDto?>.Success(leagueMember.Adapt<LeagueMemberDto?>());
        }
    }
}
