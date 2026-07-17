using FantasyFootball.Application.Common.Interfaces.Repositories;
using FantasyFootball.Domain.Entities;
using FantasyFootball.Infrastructure.Persistence.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootball.Infrastructure.Persistence.Repositories;

public class PaymentTransactionRepository : IPaymentTransactionRepository
{
    private readonly AppDbContext.AppDbContext _context;

    public PaymentTransactionRepository(AppDbContext.AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentTransactions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PaymentTransaction?> GetByIdempotencyKeyAsync(string idempotencyKey, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentTransactions
            .FirstOrDefaultAsync(x => x.IdempotencyKey == idempotencyKey, cancellationToken);
    }

    public async Task<PaymentTransaction?> GetByProviderReferenceIdAsync(string providerReferenceId, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentTransactions
            .FirstOrDefaultAsync(x => x.ProviderReferenceId == providerReferenceId, cancellationToken);
    }

    public async Task AddAsync(PaymentTransaction transaction, CancellationToken cancellationToken = default)
    {
        await _context.PaymentTransactions.AddAsync(transaction, cancellationToken);
    }

    public Task UpdateAsync(PaymentTransaction transaction, CancellationToken cancellationToken = default)
    {
        _context.PaymentTransactions.Update(transaction);
        return Task.CompletedTask;
    }
}
