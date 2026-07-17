namespace FantasyFootball.Infrastructure.Configuration
{
    public static class HangfireJobsScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            // 8. Keep Existing Jobs: Sync Teams
            RecurringJob.AddOrUpdate<IFootballDataProvider>(
                "sync-teams-daily",
                provider => provider.SyncTeamsAsync(CancellationToken.None),
                "0 2 * * *");

            // 8. Keep Existing Jobs: Sync Players
            RecurringJob.AddOrUpdate<IFootballDataProvider>(
                "sync-players-daily",
                provider => provider.SyncPlayersAsync(CancellationToken.None),
                "5 2 * * *");

            // 8. Keep Existing Jobs: Sync Fixtures (Nightly refresh)
            RecurringJob.AddOrUpdate<IFootballDataProvider>(
                "sync-fixtures-daily",
                provider => provider.SyncFixturesAsync(CancellationToken.None),
                "10 2 * * *");

            // 5. Injury Synchronization (02:15 AM, 08:15 AM, 02:15 PM, 08:15 PM)
            RecurringJob.AddOrUpdate<IFootballDataProvider>(
                "sync-injuries-daily",
                provider => provider.SyncPlayerInjuriesAsync(CancellationToken.None),
                "15 2,8,14,20 * * *");

            // 6. League Standings (Nightly synchronization)
            RecurringJob.AddOrUpdate<IFootballDataProvider>(
                "sync-standings-daily",
                provider => provider.SyncStandingsAsync(CancellationToken.None),
                "20 2 * * *");

            // 4. Daily Fixture Refresh: Schedule Dynamic Jobs (runs every day at midnight to schedule dynamic jobs for today)
            RecurringJob.AddOrUpdate<ISmartLiveSchedulerService>(
                "schedule-dynamic-daily-jobs",
                scheduler => scheduler.ScheduleDynamicDailyJobsAsync(CancellationToken.None),
                "0 0 * * *");

            // 2. Live Fixtures Sync: Every 1 minute, smartly skipping if no live matches
            RecurringJob.AddOrUpdate<ISmartLiveSchedulerService>(
                "update-live-fixtures",
                scheduler => scheduler.ExecuteLiveFixturesSyncIfActiveAsync(CancellationToken.None), 
                "*/1 * * * *");

            // 3. Live Match Events Sync: Every 1 minute, smartly skipping if no live matches
            RecurringJob.AddOrUpdate<ISmartLiveSchedulerService>(
                "update-live-match-events",
                scheduler => scheduler.ExecuteLiveMatchEventsSyncIfActiveAsync(CancellationToken.None), 
                "*/1 * * * *");

            // 7. Leaderboard Cache Refresh: Every 5 minutes
            RecurringJob.AddOrUpdate<ILeaderboardService>(
                "refresh-leaderboard-cache",
                service => service.RefreshLeaderboardCacheAsync(CancellationToken.None),
                "*/5 * * * *");
        }
    }
}
