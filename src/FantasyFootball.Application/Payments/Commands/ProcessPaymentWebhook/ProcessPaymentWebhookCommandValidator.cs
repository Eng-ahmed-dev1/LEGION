namespace FantasyFootball.Application.Payments.Commands.ProcessPaymentWebhook;

public class ProcessPaymentWebhookCommandValidator : AbstractValidator<ProcessPaymentWebhookCommand>
{
    public ProcessPaymentWebhookCommandValidator()
    {
        RuleFor(x => x.Payload)
            .NotEmpty().WithMessage("Webhook payload cannot be empty.");

        RuleFor(x => x.Signature)
            .NotEmpty().WithMessage("Webhook signature is required.");
    }
}
