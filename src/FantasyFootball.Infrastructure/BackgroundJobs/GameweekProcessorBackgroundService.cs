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

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task ProcessGameweeksAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var gameweekRepository = scope.ServiceProvider.GetRequiredService<IGameweekRepository>();
            var managerRepository = scope.ServiceProvider.GetRequiredService<IManagerRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var activeGameweeks = (await gameweekRepository.GetAllAsync())
                .Where(g => g.Deadline <= DateTime.UtcNow && !g.IsFinished)
                .ToList();

            foreach (var gameweek in activeGameweeks)
            {
                _logger.LogInformation("Processing scores for Gameweek {GameweekId}", gameweek.Id);

                var managers = await managerRepository.GetAllAsync();

                foreach (var manager in managers)
                {
                    var command = new CalculateGameweekScoreCommand(manager.Id, gameweek.Id);
                    await mediator.Send(command, cancellationToken);
                }

                _logger.LogInformation("Finished processing scores for Gameweek {GameweekId}", gameweek.Id);
            }
        }
    }
}
