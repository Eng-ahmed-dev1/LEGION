namespace FantasyFootball.Application.UseCases.Players.Commands.MarkPlayerAsUnavailable
{
    public class MarkPlayerAsUnavailableCommandValidator : AbstractValidator<MarkPlayerAsUnavailableCommand>
    {
        public MarkPlayerAsUnavailableCommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Injury reason is required.");
        }
    }
}
