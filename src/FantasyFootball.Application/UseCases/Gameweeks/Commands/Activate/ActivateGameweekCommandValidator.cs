namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.Activate
{
    public class ActivateGameweekCommandValidator : AbstractValidator<ActivateGameweekCommand>
    {
        public ActivateGameweekCommandValidator()
        {
            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
