namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateGameweekScore
{
    public class CalculateGameweekScoreCommandValidator : AbstractValidator<CalculateGameweekScoreCommand>
    {
        public CalculateGameweekScoreCommandValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("ManagerId is required.");

            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("GameweekId is required.");
        }
    }
}
