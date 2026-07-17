namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.CreatePlayerEvent
{
    public record CreatePlayerEventCommand(Guid PlayerId, Guid FixtureId, EventType EventType, int Points) : IRequest<Result<Guid>>;
}
