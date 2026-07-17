namespace FantasyFootball.Application.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentCommandValidator : AbstractValidator<CreatePaymentIntentCommand>
{
    public CreatePaymentIntentCommandValidator()
    {
        RuleFor(x => x.ManagerId)
            .NotEmpty().WithMessage("ManagerId is required.");


        RuleFor(x => x.IdempotencyKey)
            .NotEmpty().WithMessage("IdempotencyKey is required.");
    }
}
