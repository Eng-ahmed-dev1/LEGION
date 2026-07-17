namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.DeductPoints
{
    public record DeductPointsCommand(Guid PlayerEventId, int PointsToDeduct) : IRequest<Result<Unit>>;
}
