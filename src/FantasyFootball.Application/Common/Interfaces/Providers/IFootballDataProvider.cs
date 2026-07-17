namespace FantasyFootball.Application.Common.Interfaces.Providers;

public interface IFootballDataProvider
{
    Task SyncTeamsAsync(CancellationToken cancellationToken = default);
    Task SyncPlayersAsync(CancellationToken cancellationToken = default);
    Task SyncFixturesAsync(CancellationToken cancellationToken = default);
    Task SyncStandingsAsync(CancellationToken cancellationToken = default);
    Task SyncPlayerInjuriesAsync(CancellationToken cancellationToken = default);
    Task SyncMatchEventsAsync(CancellationToken cancellationToken = default);
}
