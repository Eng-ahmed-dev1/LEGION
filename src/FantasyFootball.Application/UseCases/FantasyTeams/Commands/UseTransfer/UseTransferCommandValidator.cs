namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.UseTransfer
{
    public class UseTransferCommandValidator : AbstractValidator<UseTransferCommand>
    {
        public UseTransferCommandValidator()
        {
            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("FantasyTeamId is required.");
        }
    }
}
