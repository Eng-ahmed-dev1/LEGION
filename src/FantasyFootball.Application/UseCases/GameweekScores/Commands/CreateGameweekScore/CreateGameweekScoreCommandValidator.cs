namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CreateGameweekScore
{
    public class CreateGameweekScoreCommandValidator : AbstractValidator<CreateGameweekScoreCommand>
    {
        public CreateGameweekScoreCommandValidator()
        {
            RuleFor(x => x.ManagerId)
                .NotEmpty()
                .WithMessage("Manager Id is required.");

            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
