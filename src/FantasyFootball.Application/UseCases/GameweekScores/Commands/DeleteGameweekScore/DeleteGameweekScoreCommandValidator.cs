namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.DeleteGameweekScore
{
    public class DeleteGameweekScoreCommandValidator : AbstractValidator<DeleteGameweekScoreCommand>
    {
        public DeleteGameweekScoreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Score Id is required.");
        }
    }
}
