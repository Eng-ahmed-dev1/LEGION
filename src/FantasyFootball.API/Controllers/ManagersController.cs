namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ManagersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var result = await _mediator.Send(new GetAllManagersQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetManagerById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetManagerByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("app-user/{appUserId:guid}")]
        public async Task<IActionResult> GetManagerByApplicationUserId([FromRoute] Guid appUserId)
        {
            var result = await _mediator.Send(new GetManagerByApplicationUserIdQuery(appUserId));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager([FromBody] CreateManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateManager([FromRoute] Guid id, [FromBody] UpdateManagerCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteManager([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteManagerCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/rank")]
        public async Task<IActionResult> UpdateManagerRank([FromRoute] Guid id, [FromBody] UpdateManagerRankCommand command)
        {
            var commandWithId = command with { ManagerId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetGlobalLeaderboard()
        {
            var result = await _mediator.Send(new FantasyFootball.Application.UseCases.Managers.Queries.GetGlobalLeaderboard.GetGlobalLeaderboardQuery());
            return result.ToActionResult();
        }
    }
}
