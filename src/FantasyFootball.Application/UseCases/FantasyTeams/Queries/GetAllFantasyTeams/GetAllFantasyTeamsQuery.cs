namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetAllFantasyTeams
{
    public record GetAllFantasyTeamsQuery : IRequest<Result<IReadOnlyList<FantasyTeamDto>>>;
}
