namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IManagerRepository _managerRepository;

        public ChatController(IMediator mediator, IManagerRepository managerRepository)
        {
            _mediator = mediator;
            _managerRepository = managerRepository;
        }

        public record SendMessageRequest(string RoomId, string Content, MessageType Type);
        public record EditMessageRequest(string NewContent);

        private async Task<Guid?> GetCurrentManagerIdAsync()
        {
            var appUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(appUserIdStr, out var appUserId)) return null;

            var manager = await _managerRepository.GetByApplicationUserIdAsync(appUserId);
            return manager?.Id;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var managerId = await GetCurrentManagerIdAsync();
            if (managerId == null) return Unauthorized();

            var command = new SendMessageCommand(managerId.Value, request.RoomId, request.Content, request.Type);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/edit")]
        public async Task<IActionResult> EditMessage([FromRoute] Guid id, [FromBody] EditMessageRequest request)
        {
            var managerId = await GetCurrentManagerIdAsync();
            if (managerId == null) return Unauthorized();

            var command = new EditMessageCommand(id, managerId.Value, request.NewContent);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid id)
        {
            var managerId = await GetCurrentManagerIdAsync();
            if (managerId == null) return Unauthorized();

            var command = new DeleteMessageCommand(id, managerId.Value);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}
