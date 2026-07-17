namespace FantasyFootball.Application.Payments.Commands.CreatePaymentIntent;

public class CreatePaymentIntentCommand : IRequest<Result<PaymentIntentDto>>
{
    public Guid ManagerId { get; set; }
    public string IdempotencyKey { get; set; } = string.Empty;

    public CreatePaymentIntentCommand(Guid managerId, string idempotencyKey)
    {
        ManagerId = managerId;
        IdempotencyKey = idempotencyKey;
    }
}
