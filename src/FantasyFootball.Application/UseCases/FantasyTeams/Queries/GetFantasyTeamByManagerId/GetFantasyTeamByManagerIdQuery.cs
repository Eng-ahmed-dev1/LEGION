namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetFantasyTeamByManagerId
{
    public record GetFantasyTeamByManagerIdQuery(Guid ManagerId) : IRequest<Result<FantasyTeamDto?>>;
}
