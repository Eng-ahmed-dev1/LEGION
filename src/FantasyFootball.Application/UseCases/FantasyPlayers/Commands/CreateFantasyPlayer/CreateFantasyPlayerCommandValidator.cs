namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.CreateFantasyPlayer
{
    public class CreateFantasyPlayerCommandValidator : AbstractValidator<CreateFantasyPlayerCommand>
    {
        public CreateFantasyPlayerCommandValidator()
        {
            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");
        }
    }
}
