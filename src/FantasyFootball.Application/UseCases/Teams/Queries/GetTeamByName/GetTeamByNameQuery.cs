namespace FantasyFootball.Application.UseCases.Teams.Queries.GetTeamByName
{
    public record GetTeamByNameQuery(string Name) : IRequest<Result<TeamDto?>>;
}
