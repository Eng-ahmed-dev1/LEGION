namespace FantasyFootball.Application.Payments.Commands.ProcessPaymentWebhook;

public class ProcessPaymentWebhookCommand : IRequest<Result<bool>>
{
    public string Payload { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;

    public ProcessPaymentWebhookCommand(string payload, string signature)
    {
        Payload = payload;
        Signature = signature;
    }
}
