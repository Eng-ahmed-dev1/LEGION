namespace FantasyFootball.Application.Common.Interfaces.Services;

public interface ISmartLiveSchedulerService
{
    Task ExecuteLiveFixturesSyncIfActiveAsync(CancellationToken cancellationToken = default);
    Task ExecuteLiveMatchEventsSyncIfActiveAsync(CancellationToken cancellationToken = default);
    Task ScheduleDynamicDailyJobsAsync(CancellationToken cancellationToken = default);
}
