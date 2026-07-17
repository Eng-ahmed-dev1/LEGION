namespace FantasyFootball.Application.Common.Interfaces.Services;

public interface ILeaderboardService
{
    Task RefreshLeaderboardCacheAsync(CancellationToken cancellationToken = default);
}
