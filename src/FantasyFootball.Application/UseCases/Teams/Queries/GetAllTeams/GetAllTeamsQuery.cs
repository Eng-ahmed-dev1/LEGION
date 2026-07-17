namespace FantasyFootball.Application.UseCases.Teams.Queries.GetAllTeams
{
    public record GetAllTeamsQuery : IRequest<Result<IReadOnlyList<TeamDto>>>;
}
