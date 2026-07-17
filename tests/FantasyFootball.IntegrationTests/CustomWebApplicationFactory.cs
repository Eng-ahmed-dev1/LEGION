namespace FantasyFootball.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string?>("ConnectionStrings:DefaultConnection", "Server=dummy;Database=dummy;"),
                    new KeyValuePair<string, string?>("Jwt:Key", "SuperSecretKeyForTestingTheJwtTokenGeneratorWhichNeedsToBeLongEnough123!"),
                    new KeyValuePair<string, string?>("Jwt:Issuer", "TestIssuer"),
                    new KeyValuePair<string, string?>("Jwt:Audience", "TestAudience")
                });
            });
            builder.ConfigureServices(services =>
            {
                var descriptor = services.FirstOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Remove the background service that might crash on startup with in-memory db
                var backgroundService = services.FirstOrDefault(
                    d => d.ImplementationType == typeof(FantasyFootball.Infrastructure.BackgroundJobs.GameweekProcessorBackgroundService));

                if (backgroundService != null)
                {
                    services.Remove(backgroundService);
                }

                // Replace ICacheService with a fake for tests
                var cacheDescriptors = services.Where(d => d.ServiceType == typeof(FantasyFootball.Application.Common.Interfaces.Services.ICacheService)).ToList();
                foreach (var cacheDescriptor in cacheDescriptors)
                {
                    services.Remove(cacheDescriptor);
                }
                services.AddSingleton<FantasyFootball.Application.Common.Interfaces.Services.ICacheService, FakeCacheService>();

                // Override JWT Bearer Options for tests since Program.cs captured the original config
                services.PostConfigure<Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions>(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters.ValidIssuer = "TestIssuer";
                    options.TokenValidationParameters.ValidAudience = "TestAudience";
                    options.TokenValidationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes("SuperSecretKeyForTestingTheJwtTokenGeneratorWhichNeedsToBeLongEnough123!"));
                });

                // Add mock Hangfire client
                var mockBackgroundJobClient = new Mock<IBackgroundJobClient>();
                services.AddSingleton(mockBackgroundJobClient.Object);

                // Generate unique DB name
                var dbName = Guid.NewGuid().ToString();
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(dbName);
                });
            });
        }
    }
}
