namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.CreatePlayerEvent
{
    public class CreatePlayerEventCommandValidator : AbstractValidator<CreatePlayerEventCommand>
    {
        public CreatePlayerEventCommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");

            RuleFor(x => x.FixtureId)
                .NotEmpty()
                .WithMessage("Fixture Id is required.");

            RuleFor(x => x.EventType)
                .NotEmpty()
                .WithMessage("Event type is required.")
                .IsInEnum()
                .WithMessage("Event type is invalid.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Points must be zero or greater.");
        }
    }
}
