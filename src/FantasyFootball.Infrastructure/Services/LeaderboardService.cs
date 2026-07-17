namespace FantasyFootball.Infrastructure.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly ILogger<LeaderboardService> _logger;
    private readonly ICacheService _cacheService;
    private readonly IMediator _mediator;

    public LeaderboardService(
        ILogger<LeaderboardService> logger, 
        ICacheService cacheService, 
        IMediator mediator)
    {
        _logger = logger;
        _cacheService = cacheService;
        _mediator = mediator;
    }

    public async Task RefreshLeaderboardCacheAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Leaderboard cache refresh triggered. Clearing existing cache...");
        await _cacheService.RemoveAsync("GlobalLeaderboard", cancellationToken);
        
        _logger.LogInformation("Rebuilding Global Leaderboard cache...");
        await _mediator.Send(new FantasyFootball.Application.UseCases.Managers.Queries.GetGlobalLeaderboard.GetGlobalLeaderboardQuery(), cancellationToken);
        
        _logger.LogInformation("Leaderboard cache successfully refreshed.");
    }
}
