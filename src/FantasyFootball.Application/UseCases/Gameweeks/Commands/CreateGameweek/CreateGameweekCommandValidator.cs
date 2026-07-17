namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.CreateGameweek
{
    public class CreateGameweekCommandValidator : AbstractValidator<CreateGameweekCommand>
    {
        public CreateGameweekCommandValidator()
        {
            RuleFor(x => x.Number)
                .GreaterThan(0)
                .WithMessage("Gameweek number must be greater than zero.");

            RuleFor(x => x.Deadline)
                .NotEmpty()
                .WithMessage("Gameweek deadline is required.");
        }
    }
}
