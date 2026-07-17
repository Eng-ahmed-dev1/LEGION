namespace FantasyFootball.Application.UseCases.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Result<TeamDto?>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamByIdQueryHandler(ITeamRepository teamRepository)
            => _teamRepository = teamRepository;

        public async Task<Result<TeamDto?>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.Id);
            return Result<TeamDto?>.Success(team.Adapt<TeamDto?>());
        }
    }
}
