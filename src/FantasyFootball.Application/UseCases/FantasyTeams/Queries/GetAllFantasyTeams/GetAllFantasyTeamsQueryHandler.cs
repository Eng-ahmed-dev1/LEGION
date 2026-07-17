namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetAllFantasyTeams
{
    public class GetAllFantasyTeamsQueryHandler : IRequestHandler<GetAllFantasyTeamsQuery, Result<IReadOnlyList<FantasyTeamDto>>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;

        public GetAllFantasyTeamsQueryHandler(IFantasyTeamRepository fantasyTeamRepository)
        => _fantasyTeamRepository = fantasyTeamRepository;


        async Task<Result<IReadOnlyList<FantasyTeamDto>>> IRequestHandler<GetAllFantasyTeamsQuery, Result<IReadOnlyList<FantasyTeamDto>>>.Handle(GetAllFantasyTeamsQuery request, CancellationToken cancellationToken)
        {
            var fantasyTeams = await _fantasyTeamRepository.GetAllAsync();
            return Result<IReadOnlyList<FantasyTeamDto>>.Success(fantasyTeams.Adapt<IReadOnlyList<FantasyTeamDto>>());
        }
    }
}