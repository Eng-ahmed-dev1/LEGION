namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.RemoveCaptaincy
{
    public class RemoveCaptaincyCommandValidator : AbstractValidator<RemoveCaptaincyCommand>
    {
        public RemoveCaptaincyCommandValidator()
        {
            RuleFor(x => x.playerId)
                .NotEmpty()
                .WithMessage("playerId is required.");
        }
    }
}
