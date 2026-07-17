namespace FantasyFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Microsoft.AspNetCore.RateLimiting.EnableRateLimiting("fixed")]
    public class GameweeksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GameweeksController(IMediator mediator)
        => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllGameweeks()
        {
            var result = await _mediator.Send(new GetAllGameweeksQuery());
            return result.ToActionResult();
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveGameweek()
        {
            var result = await _mediator.Send(new GetActiveGameweekQuery());
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGameweekById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetGameweekByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameweek([FromBody] CreateGameweekCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGameweek([FromRoute] Guid id, [FromBody] UpdateGameweekCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGameweek([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteGameweekCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/reschedule")]
        public async Task<IActionResult> RescheduleDeadline([FromRoute] Guid id, [FromBody] RescheduleDeadlineCommand command)
        {
            var commandWithId = command with { id = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/activate")]
        public async Task<IActionResult> ActivateGameweek([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new ActivateGameweekCommand(id));
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}/finish")]
        public async Task<IActionResult> FinishGameweek([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new FinishGameweekCommand(id));
            return result.ToActionResult();
        }

        [HttpPost("{id:guid}/fixtures")]
        public async Task<IActionResult> AddFixture([FromRoute] Guid id, [FromBody] AddFixtureToGameweekCommand command)
        {
            var commandWithId = command with { GameweekId = id };
            var result = await _mediator.Send(commandWithId);
            return result.ToActionResult();
        }

        [HttpPost("{id:guid}/trigger-autosub")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous] // Just for testing easily
        public IActionResult TriggerAutoSub([FromRoute] Guid id)
        {
            // This is where the magic happens! Hangfire takes the job and runs it in the background
            var jobId = BackgroundJob.Enqueue<IAutoSubJob>(job => job.ProcessAutoSubsAsync(id));
            
            return Ok(new { 
                Message = "Auto-Substitution job has been queued successfully!", 
                JobId = jobId 
            });
        }
    }
}