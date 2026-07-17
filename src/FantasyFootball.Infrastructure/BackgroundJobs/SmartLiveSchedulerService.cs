namespace FantasyFootball.Infrastructure.BackgroundJobs;

public class SmartLiveSchedulerService : ISmartLiveSchedulerService
{
    private readonly AppDbContext _context;
    private readonly IFootballDataProvider _footballDataProvider;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<SmartLiveSchedulerService> _logger;

    public SmartLiveSchedulerService(
        AppDbContext context,
        IFootballDataProvider footballDataProvider,
        IBackgroundJobClient backgroundJobClient,
        ILogger<SmartLiveSchedulerService> logger)
    {
        _context = context;
        _footballDataProvider = footballDataProvider;
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
    }

    private async Task<bool> HasLiveOrUpcomingFixturesAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var threshold = now.AddMinutes(15);

      
        return await _context.Fixtures.AnyAsync(f => 
            !f.IsFinished && 
            f.KickOff <= threshold && 
            f.KickOff >= now.AddHours(-4), 
            cancellationToken);
    }

    public async Task ExecuteLiveFixturesSyncIfActiveAsync(CancellationToken cancellationToken = default)
    {
        if (await HasLiveOrUpcomingFixturesAsync(cancellationToken))
        {
            _logger.LogInformation("Live fixtures detected. Executing Live Fixtures Sync.");
            await _footballDataProvider.SyncFixturesAsync(cancellationToken);
        }
        else
        {
            _logger.LogInformation("No live fixtures detected. Skipping Live Fixtures Sync.");
        }
    }

    public async Task ExecuteLiveMatchEventsSyncIfActiveAsync(CancellationToken cancellationToken = default)
    {
        if (await HasLiveOrUpcomingFixturesAsync(cancellationToken))
        {
            _logger.LogInformation("Live fixtures detected. Executing Live Match Events Sync.");
            await _footballDataProvider.SyncMatchEventsAsync(cancellationToken);
        }
        else
        {
            _logger.LogInformation("No live fixtures detected. Skipping Live Match Events Sync.");
        }
    }

    public async Task ScheduleDynamicDailyJobsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        var todaysFixtures = await _context.Fixtures
            .Where(f => f.KickOff >= today && f.KickOff < tomorrow)
            .ToListAsync(cancellationToken);

        if (todaysFixtures.Any())
        {
            var earliestKickoff = todaysFixtures.Min(f => f.KickOff);
            var latestKickoff = todaysFixtures.Max(f => f.KickOff);

            // Add fixture sync approx 2 hours before the first fixture of the day
            var refreshTime = earliestKickoff.AddHours(-2);
            if (refreshTime > DateTime.UtcNow)
            {
                _backgroundJobClient.Schedule<IFootballDataProvider>(
                    p => p.SyncFixturesAsync(CancellationToken.None),
                    refreshTime);
            }

            // Sync Standings after the last fixture finishes (approx 2.5 hours after kickoff)
            var standingsSyncTime = latestKickoff.AddHours(2.5);
            if (standingsSyncTime > DateTime.UtcNow)
            {
                _backgroundJobClient.Schedule<IFootballDataProvider>(
                    p => p.SyncStandingsAsync(CancellationToken.None),
                    standingsSyncTime);
            }
        }
    }
}
