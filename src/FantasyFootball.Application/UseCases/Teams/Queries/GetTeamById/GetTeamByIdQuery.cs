namespace FantasyFootball.Application.UseCases.Teams.Queries.GetTeamById
{
    public record GetTeamByIdQuery(Guid Id) : IRequest<Result<TeamDto?>>;
}
