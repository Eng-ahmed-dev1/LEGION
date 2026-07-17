namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class FantasyPlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FantasyPlayerController(IMediator mediator)
        => _mediator = mediator;
        [HttpPost]
        public async Task<IActionResult> CreateFantasyPlayer([FromBody] CreateFantasyPlayerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
        [HttpPut("{playerId:guid}")]
        public async Task<IActionResult> UpdateFantasyPlayer([FromRoute] Guid playerId, [FromBody] UpdateFantasyPlayerCommand command)
        {
            var commandWithId = command with { Id = playerId };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFantasyPlayer()
        {
            var result = await _mediator.Send(new GetAllFantasyPlayersQuery());
            return result.ToActionResult();
        }
        [HttpGet("{playerId:guid}")]
        public async Task<IActionResult> GetFantasyPlayerById([FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new GetFantasyPlayerByIdQuery(playerId));
            return result.ToActionResult();

        }
        [HttpGet("{teamId:guid}/team")]
        public async Task<IActionResult> GetFantasyPlayerByTeamId([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new GetByFantasyTeamIdQuery(teamId));
            return result.ToActionResult();

        }
        [HttpDelete("{playerId:guid}")]
        public async Task<IActionResult> DeleteFantasyPlayer([FromRoute] Guid playerId)
        {
            var command = new DeleteFantasyPlayerCommand(playerId);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPatch("{teamId:guid}/assign-captain/{playerId:guid}")]
        public async Task<IActionResult> AssignCaptain([FromRoute] Guid teamId, [FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new AssignCaptainCommand(teamId, playerId));
            return result.ToActionResult();
        }

        [HttpPatch("{teamId:guid}/assign-vice-captain/{playerId:guid}")]
        public async Task<IActionResult> AssignViceCaptain([FromRoute] Guid teamId, [FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new AssignViceCaptainCommand(teamId, playerId));
            return result.ToActionResult();
        }

        [HttpPatch("{playerId:guid}/remove-captaincy")]
        public async Task<IActionResult> RemoveCaptaincy([FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new RemoveCaptaincyCommand(playerId));
            return result.ToActionResult();
        }

        [HttpPatch("{playerId:guid}/move-to-bench")]
        public async Task<IActionResult> MoveToBench([FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new MoveToBenchCommand(playerId));
            return result.ToActionResult();
        }

        [HttpPatch("{playerId:guid}/move-to-starting-xi")]
        public async Task<IActionResult> MoveToStartingXI([FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new MoveToStartingXICommand(playerId));
            return result.ToActionResult();
        }
    }
}