namespace FantasyFootball.Application.Payments.Commands.ProcessPaymentWebhook;

public class ProcessPaymentWebhookCommandHandler : IRequestHandler<ProcessPaymentWebhookCommand, Result<bool>>
{
    private readonly IPaymentTransactionRepository _transactionRepository;
    private readonly IPaymentProviderService _paymentProvider;
    private readonly IManagerRepository _managerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProcessPaymentWebhookCommandHandler> _logger;

    public ProcessPaymentWebhookCommandHandler(
        IPaymentTransactionRepository transactionRepository,
        IPaymentProviderService paymentProvider,
        IManagerRepository managerRepository,
        IUnitOfWork unitOfWork,
        ILogger<ProcessPaymentWebhookCommandHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _paymentProvider = paymentProvider;
        _managerRepository = managerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(ProcessPaymentWebhookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _paymentProvider.ProcessWebhookPayload(request.Payload, request.Signature);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid webhook payload or signature detected.");
            return Result<bool>.Failure(new Error("Webhook.InvalidSignature", "Invalid signature or payload"));
        }

        if (string.IsNullOrEmpty(validationResult.ProviderReferenceId))
        {
            return Result<bool>.Success(true);
        }

        var providerReferenceId = validationResult.ProviderReferenceId;
        var paymentSuccess = validationResult.IsSuccess;

        var transaction = await _transactionRepository.GetByProviderReferenceIdAsync(providerReferenceId, cancellationToken);
        if (transaction is null)
        {
            _logger.LogWarning("Received webhook for unknown transaction: {RefId}", providerReferenceId);
            return Result<bool>.Success(true);
        }

        if (paymentSuccess)
        {
            if (transaction.Status == TransactionStatus.Completed)
                return Result<bool>.Success(true);

            transaction.MarkAsCompleted(providerReferenceId);

            var manager = await _managerRepository.GetByIdAsync(transaction.ManagerId);
            manager?.UpgradeToPremium();
        }
        else
        {
            if (transaction.Status == TransactionStatus.Failed)
                return Result<bool>.Success(true);

            transaction.MarkAsFailed(providerReferenceId);
        }

        await _transactionRepository.UpdateAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
