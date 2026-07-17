namespace FantasyFootball.Infrastructure.BackgroundJobs
{
    public class GameweekProcessorBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<GameweekProcessorBackgroundService> _logger;

        public GameweekProcessorBackgroundService(IServiceProvider serviceProvider, ILogger<GameweekProcessorBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Gameweek Processor Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessGameweeksAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing gameweeks.");
                }

                // Check every hour
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task ProcessGameweeksAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var gameweekRepository = scope.ServiceProvider.GetRequiredService<IGameweekRepository>();
            var fixtureRepository = scope.ServiceProvider.GetRequiredService<IFixtureRepository>();
            var managerRepository = scope.ServiceProvider.GetRequiredService<IManagerRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var backgroundJobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // Find gameweeks that have passed their deadline but are not finished
            var activeGameweeks = (await gameweekRepository.GetAllAsync())
                .Where(g => g.Deadline <= DateTime.UtcNow && !g.IsFinished)
                .ToList();

            foreach (var gameweek in activeGameweeks)
            {
                var fixtures = await fixtureRepository.GetByGameweekIdAsync(gameweek.Id);

                _logger.LogInformation("Processing scores for Gameweek {GameweekId}", gameweek.Id);

                var managers = await managerRepository.GetAllAsync();

                foreach (var manager in managers)
                {
                    var command = new CalculateGameweekScoreCommand(manager.Id, gameweek.Id);
                    await mediator.Send(command, cancellationToken);
                }

                _logger.LogInformation("Finished processing scores for Gameweek {GameweekId}", gameweek.Id);

                // Check if all fixtures are finished. If yes, finish the gameweek automatically and trigger Auto-Subs.
                if (fixtures.Any() && fixtures.All(f => f.IsFinished))
                {
                    _logger.LogInformation("All fixtures finished for Gameweek {GameweekId}. Finishing Gameweek and queuing Auto-Subs...", gameweek.Id);
                    
                    try
                    {
                        // To call Finish, we need the Gameweek to have fixtures loaded
                        foreach (var f in fixtures)
                        {
                            if (!gameweek.Fixtures.Contains(f))
                            {
                                gameweek.AddFixture(f);
                            }
                        }

                        gameweek.Finish();
                        gameweekRepository.Update(gameweek);
                        await unitOfWork.SaveChangesAsync(cancellationToken);

                        backgroundJobClient.Enqueue<IAutoSubJob>(job => job.ProcessAutoSubsAsync(gameweek.Id));
                        
                        _logger.LogInformation("Gameweek {GameweekId} finished successfully and Auto-Subs queued.", gameweek.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to finish Gameweek {GameweekId}", gameweek.Id);
                    }
                }
            }
        }
    }
}
