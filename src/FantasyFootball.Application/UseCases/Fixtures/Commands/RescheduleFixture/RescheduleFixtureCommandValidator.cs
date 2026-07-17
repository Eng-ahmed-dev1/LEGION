namespace FantasyFootball.Application.UseCases.Fixtures.Commands.RescheduleFixture
{
    public class RescheduleFixtureCommandValidator : AbstractValidator<RescheduleFixtureCommand>
    {
        public RescheduleFixtureCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NewKickOff).GreaterThan(DateTime.UtcNow).WithMessage("New kickoff time must be in the future");
        }
    }
}
