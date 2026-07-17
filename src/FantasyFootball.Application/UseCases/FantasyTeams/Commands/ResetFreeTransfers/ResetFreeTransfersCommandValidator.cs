namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.ResetFreeTransfers
{
    public class ResetFreeTransfersCommandValidator : AbstractValidator<ResetFreeTransfersCommand>
    {
        public ResetFreeTransfersCommandValidator()
        {
            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("FantasyTeamId is required.");
        }
    }
}
