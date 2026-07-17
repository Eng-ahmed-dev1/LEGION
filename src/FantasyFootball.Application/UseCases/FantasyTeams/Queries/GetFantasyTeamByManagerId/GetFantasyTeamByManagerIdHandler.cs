namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetFantasyTeamByManagerId
{
    public class GetFantasyTeamByManagerIdHandler : IRequestHandler<GetFantasyTeamByManagerIdQuery, Result<FantasyTeamDto?>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;

        public GetFantasyTeamByManagerIdHandler(IFantasyTeamRepository fantasyTeamRepository)
        => _fantasyTeamRepository = fantasyTeamRepository;

        public async Task<Result<FantasyTeamDto?>> Handle(GetFantasyTeamByManagerIdQuery request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByManagerIdAsync(request.ManagerId);
            return Result<FantasyTeamDto?>.Success(fantasyTeam.Adapt<FantasyTeamDto?>());
        }
    }
}
