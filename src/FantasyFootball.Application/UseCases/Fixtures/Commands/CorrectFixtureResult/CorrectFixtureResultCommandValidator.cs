namespace FantasyFootball.Application.UseCases.Fixtures.Commands.CorrectFixtureResult
{
    public class CorrectFixtureResultCommandValidator : AbstractValidator<CorrectFixtureResultCommand>
    {
        public CorrectFixtureResultCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.HomeScore).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AwayScore).GreaterThanOrEqualTo(0);
        }
    }
}
