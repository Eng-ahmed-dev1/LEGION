namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class LeaguesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaguesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            var result = await _mediator.Send(new GetAllLeaguesQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLeagueById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetLeagueByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("manager/{managerId:guid}")]
        public async Task<IActionResult> GetLeaguesByManagerId([FromRoute] Guid managerId)
        {
            var result = await _mediator.Send(new GetLeaguesByManagerIdQuery(managerId));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLeague([FromRoute] Guid id, [FromBody] UpdateLeagueCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLeague([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteLeagueCommand(id));
            return result.ToActionResult();
        }

        [HttpPost("{id:guid}/members")]
        public async Task<IActionResult> AddMember([FromRoute] Guid id, [FromBody] AddMemberToLeagueCommand command)
        {
            var commandWithId = command with { LeagueId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}/members/{managerId:guid}")]
        public async Task<IActionResult> RemoveMember([FromRoute] Guid id, [FromRoute] Guid managerId)
        {
            var result = await _mediator.Send(new RemoveMemberFromLeagueCommand(id, managerId));
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}/leaderboard")]
        public async Task<IActionResult> GetLeagueLeaderboard([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new FantasyFootball.Application.UseCases.Leagues.Queries.GetLeagueLeaderboard.GetLeagueLeaderboardQuery(id));
            return result.ToActionResult();
        }
    }
}
