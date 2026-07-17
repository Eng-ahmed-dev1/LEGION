namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.AddBonusPoints
{
    public class AddBonusPointsCommandValidator : AbstractValidator<AddBonusPointsCommand>
    {
        public AddBonusPointsCommandValidator()
        {
            RuleFor(x => x.PlayerEventId)
                .NotEmpty()
                .WithMessage("Player Event Id is required.");

            RuleFor(x => x.BonusPoints)
                .GreaterThan(0)
                .WithMessage("Bonus points must be greater than zero.");
        }
    }
}
