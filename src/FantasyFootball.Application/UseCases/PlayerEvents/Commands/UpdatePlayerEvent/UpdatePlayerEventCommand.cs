namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.UpdatePlayerEvent
{
    public record UpdatePlayerEventCommand(
        Guid Id,
        Guid PlayerId,
        Guid FixtureId,
        EventType EventType,
        int Points) : IRequest<Result<MediatR.Unit>>;
}
