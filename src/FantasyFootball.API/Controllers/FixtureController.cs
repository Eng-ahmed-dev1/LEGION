namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class FixtureController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FixtureController(IMediator mediator)
        => _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> GetAllFixtures()
        {
            var result = await _mediator.Send(new GetAllFixturesQuery());
            return result.ToActionResult();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetFixtureById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetFixtureByIdQuery(id));
            return result.ToActionResult();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFixture([FromBody] CreateFixtureCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
        [HttpPut("{id:guid}/reschedule")]
        public async Task<IActionResult> RescheduleFixture([FromRoute] Guid id, [FromBody] RescheduleFixtureCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPost("{id:guid}/record-result")]
        public async Task<IActionResult> RecordFixtureResult([FromRoute] Guid id, [FromBody] RecordFixtureResultCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/correct-result")]
        public async Task<IActionResult> CorrectFixtureResult([FromRoute] Guid id, [FromBody] CorrectFixtureResultCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFixture([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteFixtureCommand(id));
            return result.ToActionResult();
        }
        [HttpGet("gameweek/{gameweekId:guid}")]
        public async Task<IActionResult> GetByGameweekId([FromRoute] Guid gameweekId)
        {
            var result = await _mediator.Send(new GetByGameweekIdQuery(gameweekId));
            return result.ToActionResult();
        }

    }
}