namespace FantasyFootball.Application.UseCases.FantasyTeams.Queries.GetFantasyTeamById
{
    public record GetFantasyTeamByIdQuery(Guid Id) : IRequest<Result<FantasyTeamDto?>>;
}
