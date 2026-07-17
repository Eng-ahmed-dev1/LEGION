namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.AddFixture
{
    public class AddFixtureToGameweekCommandValidator : AbstractValidator<AddFixtureToGameweekCommand>
    {
        public AddFixtureToGameweekCommandValidator()
        {
            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");

            RuleFor(x => x.HomeTeamId)
                .NotEmpty()
                .WithMessage("Home team is required.");

            RuleFor(x => x.AwayTeamId)
                .NotEmpty()
                .WithMessage("Away team is required.")
                .NotEqual(x => x.HomeTeamId)
                .WithMessage("Home team and away team cannot be the same.");

            RuleFor(x => x.KickOff)
                .NotEmpty()
                .WithMessage("Kickoff time is required.");
        }
    }
}
