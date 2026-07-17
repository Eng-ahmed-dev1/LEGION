namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.AssignCaptain
{
    public class AssignCaptainCommandValidator : AbstractValidator<AssignCaptainCommand>
    {
        public AssignCaptainCommandValidator()
        {
            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("FantasyTeamId is required.");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required.");
        }
    }
}
