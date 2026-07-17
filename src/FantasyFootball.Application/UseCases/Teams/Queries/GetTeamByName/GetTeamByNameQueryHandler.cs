namespace FantasyFootball.Application.UseCases.Teams.Queries.GetTeamByName
{
    public class GetTeamByNameQueryHandler : IRequestHandler<GetTeamByNameQuery, Result<TeamDto?>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamByNameQueryHandler(ITeamRepository teamRepository)
            => _teamRepository = teamRepository;

        public async Task<Result<TeamDto?>> Handle(GetTeamByNameQuery request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByNameAsync(request.Name);
            return Result<TeamDto?>.Success(team.Adapt<TeamDto?>());
        }
    }
}
