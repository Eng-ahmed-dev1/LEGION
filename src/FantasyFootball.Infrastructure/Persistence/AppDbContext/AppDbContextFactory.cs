namespace FantasyFootball.Infrastructure.Persistence.AppDbContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../FantasyFootball.API");
            
            // Fallback for when running from the src directory or other directories
            if (!Directory.Exists(basePath))
            {
                basePath = Directory.GetCurrentDirectory();
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true);

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection") 
                ?? "Data Source=DESKTOP-6AUK3KL\\SQLEXPRESS;Initial Catalog=FantasyFootballDb;Integrated Security=True;TrustServerCertificate=True";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
