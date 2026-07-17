namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class GameweekScoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameweekScoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameweekScores()
        {
            var result = await _mediator.Send(new GetAllGameweekScoresQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGameweekScoreById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetGameweekScoreByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpGet("manager/{managerId:guid}")]
        public async Task<IActionResult> GetByManagerId([FromRoute] Guid managerId)
        {
            var result = await _mediator.Send(new GetGameweekScoresByManagerIdQuery(managerId));
            return result.ToActionResult();
        }

        [HttpGet("manager/{managerId:guid}/gameweek/{gameweekId:guid}")]
        public async Task<IActionResult> GetByManagerAndGameweek([FromRoute] Guid managerId, [FromRoute] Guid gameweekId)
        {
            var result = await _mediator.Send(new GetByManagerAndGameweekQuery(managerId, gameweekId));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameweekScore([FromBody] CreateGameweekScoreCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGameweekScore([FromRoute] Guid id, [FromBody] UpdateGameweekScoreCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGameweekScore([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteGameweekScoreCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/points")]
        public async Task<IActionResult> AddPoints([FromRoute] Guid id, [FromBody] AddPointsToGameweekScoreCommand command)
        {
            var commandWithId = command with { GameweekScoreId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateGameweekScore([FromBody] FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateGameweekScore.CalculateGameweekScoreCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPost("calculate-all")]
        public async Task<IActionResult> CalculateAllGameweekScores([FromBody] FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateAllGameweekScores.CalculateAllGameweekScoresCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("gameweek/{gameweekId:guid}/rankings")]
        public async Task<IActionResult> GetGameweekRankings([FromRoute] Guid gameweekId)
        {
            var result = await _mediator.Send(new FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekRankings.GetGameweekRankingsQuery(gameweekId));
            return result.ToActionResult();
        }
    }
}
