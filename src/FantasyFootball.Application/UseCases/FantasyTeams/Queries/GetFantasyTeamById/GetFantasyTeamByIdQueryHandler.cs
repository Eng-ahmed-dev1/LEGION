namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetFantasyTeamById
{
    public class GetFantasyTeamByIdQueryHandler : IRequestHandler<GetFantasyTeamByIdQuery, Result<FantasyTeamDto?>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;

        public GetFantasyTeamByIdQueryHandler(IFantasyTeamRepository fantasyTeamRepository)
        => _fantasyTeamRepository = fantasyTeamRepository;
        async Task<Result<FantasyTeamDto?>> IRequestHandler<GetFantasyTeamByIdQuery, Result<FantasyTeamDto?>>.Handle(GetFantasyTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var fantasyTeam = await _fantasyTeamRepository.GetByIdAsync(request.Id);
            return Result<FantasyTeamDto?>.Success(fantasyTeam.Adapt<FantasyTeamDto?>());
        }
    }
}
