namespace FantasyFootball.Application.UseCases.PlayerEvents.Events
{
    public record LiveScoreUpdateEvent(Guid PlayerId, Guid FixtureId, int PointsGained) : INotification;
}
