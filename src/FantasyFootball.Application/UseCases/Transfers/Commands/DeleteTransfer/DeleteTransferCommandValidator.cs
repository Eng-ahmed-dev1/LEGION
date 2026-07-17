namespace FantasyFootball.Application.UseCases.Transfers.Commands.DeleteTransfer
{
    public class DeleteTransferCommandValidator : AbstractValidator<DeleteTransferCommand>
    {
        public DeleteTransferCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Transfer Id is required.");
        }
    }
}
