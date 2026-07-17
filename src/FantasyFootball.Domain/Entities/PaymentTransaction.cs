using FantasyFootball.Domain.Common;
using FantasyFootball.Domain.Enums;

namespace FantasyFootball.Domain.Entities;

public class PaymentTransaction : BaseEntity
{
    public Guid ManagerId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "EGP";

    public string IdempotencyKey { get; private set; } = string.Empty;

    public string? ProviderReferenceId { get; private set; }
    public string? ClientSecret { get; private set; }

    public TransactionStatus Status { get; private set; }
    public Manager Manager { get; private set; } = null!;

    private PaymentTransaction() { }

    private PaymentTransaction(Guid managerId, decimal amount, string currency, string idempotencyKey)
    {
        Id = Guid.NewGuid();
        ManagerId = managerId;
        Amount = amount;
        Currency = currency;
        IdempotencyKey = idempotencyKey;
        Status = TransactionStatus.Pending;
    }

    public static PaymentTransaction Create(Guid managerId, decimal amount, string currency, string idempotencyKey)
    {
        return new PaymentTransaction(managerId, amount, currency, idempotencyKey);
    }

    public void SetPaymentIntent(string providerReferenceId, string clientSecret)
    {
        ProviderReferenceId = providerReferenceId;
        ClientSecret = clientSecret;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted(string? providerReferenceId = null)
    {
        if (Status == TransactionStatus.Completed)
            return;

        if (Status != TransactionStatus.Pending)
            throw new DomainException("Only pending transactions can be completed.");

        Status = TransactionStatus.Completed;

        if (!string.IsNullOrEmpty(providerReferenceId))
            ProviderReferenceId = providerReferenceId;

        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string? providerReferenceId = null)
    {
        if (Status == TransactionStatus.Failed)
            return;

        if (Status != TransactionStatus.Pending)
            throw new DomainException("Only pending transactions can be marked as failed.");

        Status = TransactionStatus.Failed;

        if (!string.IsNullOrEmpty(providerReferenceId))
            ProviderReferenceId = providerReferenceId;

        UpdatedAt = DateTime.UtcNow;
    }
}
