namespace FantasyFootball.Application.UseCases.Transfers.Commands.CreateTransfer
{
    public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
    {
        public CreateTransferCommandValidator()
        {
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

            RuleFor(x => x)
                .Must(x => x.PlayerInId != x.PlayerOutId)
                .WithMessage("Player In Id and Player Out Id must be different.");
        }
    }
}
