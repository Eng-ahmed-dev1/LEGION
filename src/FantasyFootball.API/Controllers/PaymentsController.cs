namespace FantasyFootball.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IManagerRepository _managerRepository;

    public PaymentsController(IMediator mediator, IManagerRepository managerRepository)
    {
        _mediator = mediator;
        _managerRepository = managerRepository;
    }

    [HttpPost("intent")]
    public async Task<IActionResult> CreatePaymentIntent(
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
            return BadRequest("Idempotency-Key header is required.");

        var managerId = await GetCurrentManagerIdAsync();
        if (managerId is null)
            return Unauthorized();

        var command = new CreatePaymentIntentCommand(managerId.Value, idempotencyKey);
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<IActionResult> PaymentWebhook()
    {
        using var streamReader = new StreamReader(Request.Body);
        var payload = await streamReader.ReadToEndAsync();
        var signature = Request.Headers["Stripe-Signature"].ToString();

        var command = new ProcessPaymentWebhookCommand(payload, signature);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    private async Task<Guid?> GetCurrentManagerIdAsync()
    {
        var appUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(appUserIdStr, out var appUserId))
            return null;

        var manager = await _managerRepository.GetByApplicationUserIdAsync(appUserId);
        return manager?.Id;
    }
}
