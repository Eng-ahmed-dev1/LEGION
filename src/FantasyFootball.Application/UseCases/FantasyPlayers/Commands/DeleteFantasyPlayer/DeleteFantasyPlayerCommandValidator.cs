namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.DeleteFantasyPlayer
{
    public class DeleteFantasyPlayerCommandValidator : AbstractValidator<DeleteFantasyPlayerCommand>
    {
        public DeleteFantasyPlayerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fantasy Player Id is required.");
        }
    }
}
