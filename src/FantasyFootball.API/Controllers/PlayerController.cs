namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers([FromQuery] PlayerQueryParameters parameters)
        {
            var result = await _mediator.Send(new GetAllPlayersQuery(parameters));
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPlayerById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPlayerByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("team/{teamId:guid}")]
        public async Task<IActionResult> GetByTeamId([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new GetByTeamIdQuery(teamId));
            return result.ToActionResult();
        }

        [HttpGet("position/{position}")]
        public async Task<IActionResult> GetByPosition([FromRoute] PlayerPosition position)
        {
            var result = await _mediator.Send(new GetByPositionQuery(position));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlayer([FromRoute] Guid id, [FromBody] UpdatePlayerCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeletePlayerCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/available")]
        public async Task<IActionResult> MarkAsAvailable([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new MarkPlayerAsAvailableCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/unavailable")]
        public async Task<IActionResult> MarkAsUnavailable([FromRoute] Guid id, [FromBody] MarkPlayerAsUnavailableCommand command)
        {
            var commandWithId = command with { PlayerId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }
    }
}
