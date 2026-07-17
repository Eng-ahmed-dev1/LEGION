namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.AddPoints
{
    public class AddPointsToGameweekScoreCommandValidator : AbstractValidator<AddPointsToGameweekScoreCommand>
    {
        public AddPointsToGameweekScoreCommandValidator()
        {
            RuleFor(x => x.GameweekScoreId)
                .NotEmpty()
                .WithMessage("GameweekScore Id is required.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Points cannot be negative.");
        }
    }
}
