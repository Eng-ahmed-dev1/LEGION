namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.RescheduleDeadline
{
    public class RescheduleDeadlineCommandValidator : AbstractValidator<RescheduleDeadlineCommand>
    {
        public RescheduleDeadlineCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty()
                .WithMessage("id is required.");

            RuleFor(x => x.newDeadline)
                .NotEmpty()
                .WithMessage("newDeadline is required.");
        }
    }
}
