namespace FantasyFootball.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Only Admins can manually trigger syncs
public class SyncController : ControllerBase
{
    private readonly IFootballDataProvider _provider;

    public SyncController(IFootballDataProvider provider)
    {
        _provider = provider;
    }

    [HttpPost("teams")]
    public async Task<IActionResult> SyncTeams(CancellationToken cancellationToken)
    {
        await _provider.SyncTeamsAsync(cancellationToken);
        return Result<string>.Success("Teams sync started successfully.").ToActionResult();
    }

    [HttpPost("players")]
    public async Task<IActionResult> SyncPlayers(CancellationToken cancellationToken)
    {
        await _provider.SyncPlayersAsync(cancellationToken);
        return Result<string>.Success("Players sync started successfully.").ToActionResult();
    }

    [HttpPost("fixtures")]
    public async Task<IActionResult> SyncFixtures(CancellationToken cancellationToken)
    {
        await _provider.SyncFixturesAsync(cancellationToken);
        return Result<string>.Success("Fixtures sync started successfully.").ToActionResult();
    }
}
