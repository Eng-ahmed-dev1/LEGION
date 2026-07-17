namespace FantasyFootball.Application.UseCases.Teams.Queries.GetAllTeams
{
    public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, Result<IReadOnlyList<TeamDto>>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetAllTeamsQueryHandler(ITeamRepository teamRepository)
            => _teamRepository = teamRepository;

        public async Task<Result<IReadOnlyList<TeamDto>>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetAllAsync();
            return Result<IReadOnlyList<TeamDto>>.Success(teams.Adapt<IReadOnlyList<TeamDto>>());
        }
    }
}
