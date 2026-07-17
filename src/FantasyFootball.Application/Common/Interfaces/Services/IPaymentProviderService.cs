namespace FantasyFootball.Application.Common.Interfaces.Services;

public interface IPaymentProviderService
{
    /// <summary>
    /// Communicates with the external payment gateway (e.g. Stripe, Paymob)
    /// to create a payment intent/session and returns the Client Secret or Checkout URL.
    /// </summary>
    Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency, string idempotencyKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates the webhook signature from the payment provider to ensure it's not spoofed.
    /// </summary>
    WebhookValidationResult ProcessWebhookPayload(string payload, string signature);
}

public record PaymentIntentResult(string ClientSecret, string ProviderReferenceId);
public record WebhookValidationResult(bool IsValid, string? ProviderReferenceId, bool IsSuccess);
