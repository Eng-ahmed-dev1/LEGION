namespace FantasyFootball.Application.UseCases.Fixtures.Commands.CreateFixture
{
    public class CreateFixtureCommandValidator : AbstractValidator<CreateFixtureCommand>
    {
        public CreateFixtureCommandValidator()
        {
            RuleFor(x => x.HomeTeamId)
                .NotEmpty()
                .WithMessage("Home Team Id is required.");

            RuleFor(x => x.AwayTeamId)
                .NotEmpty()
                .WithMessage("Away Team Id is required.");

            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");

            RuleFor(x => x.KickOff)
                .NotEmpty()
                .WithMessage("Kick-off time is required.");

            RuleFor(x => x)
                .Must(x => x.HomeTeamId != x.AwayTeamId)
                .WithMessage("Home Team Id and Away Team Id must be different.");
        }
    }
}
