namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class FantasyTeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FantasyTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFantasyTeam([FromBody] CreateFantasyTeamCommand teamCommand)
        {
            var result = await _mediator.Send(teamCommand);
            return result.ToActionResult();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFantasyTeams()
        {
            var result = await _mediator.Send(new GetAllFantasyTeamsQuery());
            return result.ToActionResult();
        }
        [HttpDelete("{teamId:guid}")]
        public async Task<IActionResult> DeleteFantasyTeam([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new DeleteFantasyTeamCommand(teamId));
            return result.ToActionResult();

        }
        [HttpPut("{teamId:guid}")]
        public async Task<IActionResult> UpdateFantasyTeam([FromRoute] Guid teamId, [FromBody] UpdateFantasyTeamCommand teamCommand)
        {
            var commandWithId = teamCommand with { Id = teamId };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }
        [HttpGet("{teamId:guid}")]
        public async Task<IActionResult> GetFantasyTeamById([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new GetFantasyTeamByIdQuery(teamId));
            return result.ToActionResult();
        }
        [HttpGet("manager/{managerId:guid}")]
        public async Task<IActionResult> GetFantasyTeamByManagerId([FromRoute] Guid managerId)
        {
            var result = await _mediator.Send(new GetFantasyTeamByManagerIdQuery(managerId));
            return result.ToActionResult();
        }

        [HttpPost("{teamId:guid}/players")]
        public async Task<IActionResult> AddPlayer([FromRoute] Guid teamId, [FromBody] AddPlayerToFantasyTeamCommand command)
        {
            var commandWithId = command with { FantasyTeamId = teamId };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{teamId:guid}/players/{playerId:guid}")]
        public async Task<IActionResult> RemovePlayer([FromRoute] Guid teamId, [FromRoute] Guid playerId)
        {
            var result = await _mediator.Send(new RemovePlayerFromFantasyTeamCommand(teamId, playerId));
            return result.ToActionResult();
        }

        [HttpPost("{teamId:guid}/reset-transfers")]
        public async Task<IActionResult> ResetFreeTransfers([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new ResetFreeTransfersCommand(teamId));
            return result.ToActionResult();
        }

        [HttpPost("{teamId:guid}/use-transfer")]
        public async Task<IActionResult> UseTransfer([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new UseTransferCommand(teamId));
            return result.ToActionResult();
        }

        [HttpPost("{teamId:guid}/chips")]
        public async Task<IActionResult> PlayChip([FromRoute] Guid teamId, [FromBody] FantasyFootball.Application.UseCases.FantasyTeams.Commands.PlayChip.PlayChipCommand command)
        {
            var commandWithId = command with { FantasyTeamId = teamId };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPost("{teamId:guid}/toggle-auto-sub")]
        public async Task<IActionResult> ToggleAutoSub([FromRoute] Guid teamId)
        {
            var result = await _mediator.Send(new FantasyFootball.Application.UseCases.FantasyTeams.Commands.ToggleAutoSub.ToggleAutoSubCommand(teamId));
            return result.ToActionResult();
        }
    }
}