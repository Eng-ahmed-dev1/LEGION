namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetFantasyPlayerById
{
    public record GetFantasyPlayerByIdQuery(Guid Id) : IRequest<Result<FantasyPlayerDto?>>;
}
