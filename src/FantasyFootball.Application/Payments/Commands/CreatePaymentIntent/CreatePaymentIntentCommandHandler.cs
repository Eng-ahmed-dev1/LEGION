namespace FantasyFootball.Application.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentCommandHandler : IRequestHandler<CreatePaymentIntentCommand, Result<PaymentIntentDto>>
{
    private readonly IPaymentTransactionRepository _transactionRepository;
    private readonly IPaymentProviderService _paymentProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PaymentSettings _paymentSettings;
    private readonly ILogger<CreatePaymentIntentCommandHandler> _logger;

    public CreatePaymentIntentCommandHandler(
        IPaymentTransactionRepository transactionRepository,
        IPaymentProviderService paymentProvider,
        IUnitOfWork unitOfWork,
        IOptions<PaymentSettings> paymentSettings,
        ILogger<CreatePaymentIntentCommandHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _paymentProvider = paymentProvider;
        _unitOfWork = unitOfWork;
        _paymentSettings = paymentSettings.Value;
        _logger = logger;
    }

    public async Task<Result<PaymentIntentDto>> Handle(CreatePaymentIntentCommand request, CancellationToken cancellationToken)
    {
        var existingTransaction = await _transactionRepository.GetByIdempotencyKeyAsync(request.IdempotencyKey, cancellationToken);
        if (existingTransaction is not null)
        {
            return await HandleExistingTransactionAsync(existingTransaction, request, cancellationToken);
        }

        var transaction = PaymentTransaction.Create(
            request.ManagerId,
            _paymentSettings.PremiumPrice,
            _paymentSettings.Currency,
            request.IdempotencyKey);

        await _transactionRepository.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await CreateProviderIntentAsync(transaction, request, cancellationToken);
    }

    private async Task<Result<PaymentIntentDto>> HandleExistingTransactionAsync(
        PaymentTransaction existingTransaction,
        CreatePaymentIntentCommand request,
        CancellationToken cancellationToken)
    {
        if (existingTransaction.ManagerId != request.ManagerId)
        {
            return Result<PaymentIntentDto>.Failure(new Error(
                "Payment.Duplicate",
                "This idempotency key belongs to another user."));
        }

        if (existingTransaction.Status == TransactionStatus.Completed)
        {
            return Result<PaymentIntentDto>.Failure(new Error(
                "Payment.AlreadyCompleted",
                "This payment has already been completed."));
        }

        if (!string.IsNullOrEmpty(existingTransaction.ClientSecret))
        {
            return Result<PaymentIntentDto>.Success(
                new PaymentIntentDto(existingTransaction.ClientSecret, request.IdempotencyKey));
        }

        if (existingTransaction.Status != TransactionStatus.Pending)
        {
            return Result<PaymentIntentDto>.Failure(new Error(
                "Payment.Failed",
                "Previous payment attempt failed. Use a new idempotency key."));
        }

        return await CreateProviderIntentAsync(existingTransaction, request, cancellationToken);
    }

    private async Task<Result<PaymentIntentDto>> CreateProviderIntentAsync(
        PaymentTransaction transaction,
        CreatePaymentIntentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var paymentResult = await _paymentProvider.CreatePaymentIntentAsync(
                _paymentSettings.PremiumPrice,
                _paymentSettings.Currency,
                request.IdempotencyKey,
                cancellationToken);

            transaction.SetPaymentIntent(paymentResult.ProviderReferenceId, paymentResult.ClientSecret);

            await _transactionRepository.UpdateAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<PaymentIntentDto>.Success(
                new PaymentIntentDto(paymentResult.ClientSecret, request.IdempotencyKey));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Payment provider failed for idempotency key {IdempotencyKey}", request.IdempotencyKey);

            transaction.MarkAsFailed();
            await _transactionRepository.UpdateAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<PaymentIntentDto>.Failure(new Error(
                "Payment.ProviderError",
                "Failed to create payment intent. Please try again."));
        }
    }
}
