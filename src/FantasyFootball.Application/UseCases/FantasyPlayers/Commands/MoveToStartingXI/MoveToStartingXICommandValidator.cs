namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.MoveToStartingXI
{
    public class MoveToStartingXICommandValidator : AbstractValidator<MoveToStartingXICommand>
    {
        public MoveToStartingXICommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required.");
        }
    }
}
