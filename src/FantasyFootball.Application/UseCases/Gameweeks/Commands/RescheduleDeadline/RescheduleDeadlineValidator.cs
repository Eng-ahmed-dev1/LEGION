namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.RescheduleDeadline
{
    public class RescheduleDeadlineValidator : AbstractValidator<RescheduleDeadlineCommand>
    {
        public RescheduleDeadlineValidator()
        {
            RuleFor(x => x.newDeadline)
            .GreaterThan(DateTime.Now)
            .NotEmpty()
            .WithMessage("The new deadline must be not empty");
        }

    }
}