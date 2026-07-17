namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.UpdateGameweek
{
    public class UpdateGameweekCommandValidator : AbstractValidator<UpdateGameweekCommand>
    {
        public UpdateGameweekCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");

            RuleFor(x => x.Number)
                .GreaterThan(0)
                .WithMessage("Gameweek number must be greater than zero.");

            RuleFor(x => x.Deadline)
                .NotEmpty()
                .WithMessage("Deadline is required.");
        }
    }
}
