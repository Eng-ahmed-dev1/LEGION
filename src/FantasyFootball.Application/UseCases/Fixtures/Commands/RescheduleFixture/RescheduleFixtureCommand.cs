namespace FantasyFootball.Application.UseCases.Fixtures.Commands.RescheduleFixture
{
    public record RescheduleFixtureCommand(Guid Id, DateTime NewKickOff) : IRequest<Result<Unit>>;
}
