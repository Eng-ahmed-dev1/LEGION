namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.DeductPoints
{
    public class DeductPointsCommandValidator : AbstractValidator<DeductPointsCommand>
    {
        public DeductPointsCommandValidator()
        {
            RuleFor(x => x.PlayerEventId)
                .NotEmpty()
                .WithMessage("Player Event Id is required.");

            RuleFor(x => x.PointsToDeduct)
                .GreaterThan(0)
                .WithMessage("Points to deduct must be greater than zero.");
        }
    }
}
