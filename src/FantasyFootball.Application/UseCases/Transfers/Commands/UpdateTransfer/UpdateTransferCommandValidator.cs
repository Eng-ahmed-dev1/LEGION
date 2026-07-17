namespace FantasyFootball.Application.UseCases.Transfers.Commands.UpdateTransfer
{
    public class UpdateTransferCommandValidator : AbstractValidator<UpdateTransferCommand>
    {
        public UpdateTransferCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Transfer Id is required.");

            RuleFor(x => x.FantasyTeamId)
                .NotEmpty()
                .WithMessage("Fantasy Team Id is required.");

            RuleFor(x => x.PlayerInId)
                .NotEmpty()
                .WithMessage("Player In Id is required.");

            RuleFor(x => x.PlayerOutId)
                .NotEmpty()
                .WithMessage("Player Out Id is required.");

            RuleFor(x => x.GameweekId)
                .NotEmpty()
                .WithMessage("Gameweek Id is required.");
        }
    }
}
