namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.UpdateFantasyPlayer
{
    public class UpdateFantasyPlayerCommandValidator : AbstractValidator<UpdateFantasyPlayerCommand>
    {
        public UpdateFantasyPlayerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Player Id is required.");

            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required.");
        }
    }
}
