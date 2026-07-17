namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.UpdateGameweekScore
{
    public class UpdateGameweekScoreCommandValidator : AbstractValidator<UpdateGameweekScoreCommand>
    {
        public UpdateGameweekScoreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Score Id is required.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Points must be non-negative.");

            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");

            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
