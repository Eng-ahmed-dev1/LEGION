namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class TransfersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransfersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransfers()
        {
            var result = await _mediator.Send(new GetAllTransfersQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTransferById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetTransferByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("fantasy-team/{fantasyTeamId:guid}")]
        public async Task<IActionResult> GetTransfersByFantasyTeamId([FromRoute] Guid fantasyTeamId)
        {
            var result = await _mediator.Send(new GetTransfersByFantasyTeamIdQuery(fantasyTeamId));
            return result.ToActionResult();
        }

        [HttpGet("gameweek/{gameweekId:guid}")]
        public async Task<IActionResult> GetTransfersByGameweekId([FromRoute] Guid gameweekId)
        {
            var result = await _mediator.Send(new GetTransfersByGameweekIdQuery(gameweekId));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransfer([FromRoute] Guid id, [FromBody] UpdateTransferCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTransfer([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteTransferCommand(id));
            return result.ToActionResult();
        }
    }
}
