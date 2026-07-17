namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class LeagueMembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeagueMembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeagueMembers()
        {
            var result = await _mediator.Send(new GetAllLeagueMemberQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLeagueMemberById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetLeagueMemberByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("manager/{managerId:guid}")]
        public async Task<IActionResult> GetByManagerId([FromRoute] Guid managerId)
        {
            var result = await _mediator.Send(new GetLeagueMembersByManagerIdQuery(managerId));
            return result.ToActionResult();
        }

        [HttpGet("league/{leagueId:guid}")]
        public async Task<IActionResult> GetByLeagueId([FromRoute] Guid leagueId)
        {
            var result = await _mediator.Send(new GetByLeagueIdQuery(leagueId));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeagueMember([FromBody] CreateLeagueMemberCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLeagueMember([FromRoute] Guid id, [FromBody] UpdateLeagueMemberCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLeagueMember([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteLeagueMemberCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/points")]
        public async Task<IActionResult> AddPoints([FromRoute] Guid id, [FromBody] AddPointsToLeagueMemberCommand command)
        {
            var commandWithId = command with { LeagueMemberId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }
    }
}
