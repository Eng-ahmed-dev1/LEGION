namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.DeletePlayerEvent
{
    public class DeletePlayerEventCommandValidator : AbstractValidator<DeletePlayerEventCommand>
    {
        public DeletePlayerEventCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Player Event Id is required.");
        }
    }
}
