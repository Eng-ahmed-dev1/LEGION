namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class PlayerEventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayerEvents()
        {
            var result = await _mediator.Send(new GetAllPlayerEventsQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPlayerEventById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPlayerEventByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("fixture/{fixtureId:guid}")]
        public async Task<IActionResult> GetPlayerEventsByFixtureId([FromRoute] Guid fixtureId)
        {
            var result = await _mediator.Send(new GetPlayerEventsByFixtureIdQuery(fixtureId));
            return result.ToActionResult();
        }

        [HttpGet("player/{playerId:guid}")]
        public async Task<IActionResult> GetPlayerEventsByPlayerId([FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new GetPlayerEventsByPlayerIdQuery(playerId));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayerEvent([FromBody] CreatePlayerEventCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlayerEvent([FromRoute] Guid id, [FromBody] UpdatePlayerEventCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlayerEvent([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeletePlayerEventCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/bonus-points")]
        public async Task<IActionResult> AddBonusPoints([FromRoute] Guid id, [FromBody] AddBonusPointsCommand command)
        {
            var commandWithId = command with { PlayerEventId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/deduct-points")]
        public async Task<IActionResult> DeductPoints([FromRoute] Guid id, [FromBody] DeductPointsCommand command)
        {
            var commandWithId = command with { PlayerEventId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }
    }
}
