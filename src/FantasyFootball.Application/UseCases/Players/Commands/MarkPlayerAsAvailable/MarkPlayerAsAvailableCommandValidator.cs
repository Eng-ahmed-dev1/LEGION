namespace FantasyFootball.Application.UseCases.Players.Commands.MarkPlayerAsAvailable
{
    public class MarkPlayerAsAvailableCommandValidator : AbstractValidator<MarkPlayerAsAvailableCommand>
    {
        public MarkPlayerAsAvailableCommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");
        }
    }
}
