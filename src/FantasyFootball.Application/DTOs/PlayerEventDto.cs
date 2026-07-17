namespace FantasyFootball.Application.DTOs
{
    public record PlayerEventDto(
        Guid Id,
        Guid PlayerId,
        Guid FixtureId,
        string EventType,
        int Points);
}