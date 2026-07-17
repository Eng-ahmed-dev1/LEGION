namespace FantasyFootball.Application.UseCases.Fixtures.Commands.RecordFixtureResult
{
    public class RecordFixtureResultCommandValidator : AbstractValidator<RecordFixtureResultCommand>
    {
        public RecordFixtureResultCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.HomeScore).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AwayScore).GreaterThanOrEqualTo(0);
        }
    }
}
