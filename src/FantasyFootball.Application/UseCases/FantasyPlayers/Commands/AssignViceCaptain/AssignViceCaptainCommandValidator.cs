namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.AssignViceCaptain
{
    public class AssignViceCaptainCommandValidator : AbstractValidator<AssignViceCaptainCommand>
    {
        public AssignViceCaptainCommandValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required.");
        }
    }
}
