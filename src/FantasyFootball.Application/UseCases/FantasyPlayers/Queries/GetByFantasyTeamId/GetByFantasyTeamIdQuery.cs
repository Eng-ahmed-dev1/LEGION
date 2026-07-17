namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetByFantasyTeamId
{
    public record GetByFantasyTeamIdQuery(Guid Id) : IRequest<Result<FantasyPlayerDto?>>;
}
