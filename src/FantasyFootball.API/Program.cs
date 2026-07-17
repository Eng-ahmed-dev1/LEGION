if (!Environment.GetCommandLineArgs().Contains("--environment=Testing") && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Testing")
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("Logs/bootstrap-log-.txt", rollingInterval: RollingInterval.Day)
        .CreateBootstrapLogger();
}

try
{
    var builder = WebApplication.CreateBuilder(args);
    var isTesting = builder.Environment.IsEnvironment("Testing");

    if (!isTesting)
    {
        builder.Host.UseSerilog((context, services, configuration) => 
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: context.Configuration.GetConnectionString("DefaultConnection"),
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true }
                );
        });
    }

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, isTesting);
var app = builder.Build();

await app.UseInfrastructureAsync(isTesting);

app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("STARTUP FATAL ERROR: " + ex.ToString());
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
