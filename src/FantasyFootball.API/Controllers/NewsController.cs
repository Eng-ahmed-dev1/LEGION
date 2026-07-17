namespace FantasyFootball.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("articles")]
    public async Task<IActionResult> CreateNewsArticle([FromBody] CreateNewsArticleCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("players")]
    public async Task<IActionResult> AddPlayerNews([FromBody] AddPlayerNewsCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet("articles")]
    public async Task<IActionResult> GetNewsArticles()
    {
        var result = await _mediator.Send(new FantasyFootball.Application.UseCases.News.Queries.GetNewsArticles.GetNewsArticlesQuery());
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet("articles/{id}")]
    public async Task<IActionResult> GetNewsArticleById(Guid id)
    {
        var result = await _mediator.Send(new FantasyFootball.Application.UseCases.News.Queries.GetNewsArticleById.GetNewsArticleByIdQuery(id));
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet("players/latest")]
    public async Task<IActionResult> GetLatestPlayerNews()
    {
        var result = await _mediator.Send(new FantasyFootball.Application.UseCases.Players.Queries.GetLatestPlayerNews.GetLatestPlayerNewsQuery());
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet("players/{playerId}")]
    public async Task<IActionResult> GetPlayerNews(Guid playerId)
    {
        var result = await _mediator.Send(new FantasyFootball.Application.UseCases.Players.Queries.GetPlayerNews.GetPlayerNewsQuery(playerId));
        return result.ToActionResult();
    }
}
