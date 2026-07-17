using FantasyFootball.Application.Common.Interfaces.Services;
using FantasyFootball.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;

namespace FantasyFootball.Infrastructure.Services;

public class StripePaymentProviderService : IPaymentProviderService
{
    private readonly StripeSettings _stripeSettings;
    private readonly ILogger<StripePaymentProviderService> _logger;

    public StripePaymentProviderService(
        IOptions<StripeSettings> stripeSettings,
        ILogger<StripePaymentProviderService> logger)
    {
        _stripeSettings = stripeSettings.Value;
        _logger = logger;
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    }

    public async Task<PaymentIntentResult> CreatePaymentIntentAsync(
        decimal amount,
        string currency,
        string idempotencyKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = ToStripeAmount(amount),
                Currency = currency.ToLowerInvariant(),
                Metadata = new Dictionary<string, string>
                {
                    { "IdempotencyKey", idempotencyKey }
                }
            };

            var requestOptions = new RequestOptions
            {
                IdempotencyKey = idempotencyKey
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options, requestOptions, cancellationToken);

            return new PaymentIntentResult(paymentIntent.ClientSecret, paymentIntent.Id);
        }
        catch (StripeException e)
        {
            _logger.LogError(e, "Stripe exception occurred while creating payment intent.");
            throw;
        }
    }

    public WebhookValidationResult ProcessWebhookPayload(string payload, string signature)
    {
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                payload,
                signature,
                _stripeSettings.WebhookSecret);

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                return new WebhookValidationResult(true, paymentIntent?.Id, true);
            }

            if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                return new WebhookValidationResult(true, paymentIntent?.Id, false);
            }

            return new WebhookValidationResult(true, null, false);
        }
        catch (StripeException e)
        {
            _logger.LogError(e, "Stripe exception during webhook signature validation.");
            return new WebhookValidationResult(false, null, false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "General exception during webhook signature validation.");
            return new WebhookValidationResult(false, null, false);
        }
    }

    private static long ToStripeAmount(decimal amount) =>
        decimal.ToInt64(decimal.Round(amount * 100, 0, MidpointRounding.AwayFromZero));
}
