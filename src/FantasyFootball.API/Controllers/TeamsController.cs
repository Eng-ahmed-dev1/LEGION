namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class TeamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var result = await _mediator.Send(new GetAllTeamsQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTeamById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetTeamByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetTeamByName([FromRoute] string name)
        {
            var result = await _mediator.Send(new GetTeamByNameQuery(name));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTeam([FromRoute] Guid id, [FromBody] UpdateTeamCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteTeamCommand(id));
            return result.ToActionResult();
        }
    }
}
