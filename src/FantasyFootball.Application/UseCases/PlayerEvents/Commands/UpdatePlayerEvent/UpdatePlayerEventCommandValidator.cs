namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.UpdatePlayerEvent
{
    public class UpdatePlayerEventCommandValidator : AbstractValidator<UpdatePlayerEventCommand>
    {
        public UpdatePlayerEventCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Player Event Id is required.");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");

            RuleFor(x => x.FixtureId)
                .NotEmpty()
                .WithMessage("Fixture Id is required.");

            RuleFor(x => x.EventType)
                .IsInEnum()
                .WithMessage("Invalid event type.");
        }
    }
}
