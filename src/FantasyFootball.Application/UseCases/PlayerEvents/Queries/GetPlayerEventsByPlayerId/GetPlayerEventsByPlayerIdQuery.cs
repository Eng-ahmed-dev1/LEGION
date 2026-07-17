namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventsByPlayerId
{
    public record GetPlayerEventsByPlayerIdQuery(Guid PlayerId) : IRequest<Result<IReadOnlyList<PlayerEventDto>>>;
}
