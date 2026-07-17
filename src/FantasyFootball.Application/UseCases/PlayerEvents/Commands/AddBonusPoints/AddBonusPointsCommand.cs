namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.AddBonusPoints
{
    public record AddBonusPointsCommand(Guid PlayerEventId, int BonusPoints) : IRequest<Result<Unit>>;
}
