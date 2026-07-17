namespace FantasyFootball.Infrastructure.Persistence
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FantasyFootball.Infrastructure.Persistence.AppDbContext.AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<FantasyFootball.Infrastructure.Persistence.AppDbContext.AppDbContext>>();

            try
            {
                if (await context.Teams.AnyAsync())
                {
                    logger.LogInformation("Database already seeded.");
                    return;
                }

                logger.LogInformation("Seeding Egyptian League Data...");

                // 1. Seed Teams
                var ahly = Team.Create("Al Ahly SC", "AHL");
                var zamalek = Team.Create("Zamalek SC", "ZAM");
                var pyramids = Team.Create("Pyramids FC", "PYR");
                var masry = Team.Create("Al Masry", "MAS");
                
                context.Teams.AddRange(ahly, zamalek, pyramids, masry);
                await context.SaveChangesAsync();

                // 2. Seed Players
                var players = new List<Player>
                {
                    // Al Ahly
                    Player.Create("Mohamed", "El Shenawy", PlayerPosition.Goalkeeper, new Price(5.5m), ahly.Id),
                    Player.Create("Mohamed", "Abdelmonem", PlayerPosition.Defender, new Price(5.0m), ahly.Id),
                    Player.Create("Ali", "Maaloul", PlayerPosition.Defender, new Price(6.0m), ahly.Id),
                    Player.Create("Emam", "Ashour", PlayerPosition.Midfielder, new Price(7.5m), ahly.Id),
                    Player.Create("Hussein", "El Shahat", PlayerPosition.Midfielder, new Price(7.0m), ahly.Id),
                    Player.Create("Wessam", "Abou Ali", PlayerPosition.Forward, new Price(8.0m), ahly.Id),

                    // Zamalek
                    Player.Create("Mohamed", "Awad", PlayerPosition.Goalkeeper, new Price(5.0m), zamalek.Id),
                    Player.Create("Omar", "Gaber", PlayerPosition.Defender, new Price(4.5m), zamalek.Id),
                    Player.Create("Ahmed", "Sayed Zizo", PlayerPosition.Midfielder, new Price(9.0m), zamalek.Id),
                    Player.Create("Mahmoud", "Shikabala", PlayerPosition.Midfielder, new Price(6.0m), zamalek.Id),
                    Player.Create("Seif", "Jaziri", PlayerPosition.Forward, new Price(7.0m), zamalek.Id),
                    
                    // Pyramids
                    Player.Create("Ahmed", "El Shenawy", PlayerPosition.Goalkeeper, new Price(5.0m), pyramids.Id),
                    Player.Create("Mohamed", "Chibi", PlayerPosition.Defender, new Price(5.5m), pyramids.Id),
                    Player.Create("Abdallah", "El Said", PlayerPosition.Midfielder, new Price(7.0m), pyramids.Id),
                    Player.Create("Ramadan", "Sobhi", PlayerPosition.Midfielder, new Price(7.5m), pyramids.Id),
                    Player.Create("Fiston", "Mayele", PlayerPosition.Forward, new Price(8.5m), pyramids.Id),

                    // Al Masry
                    Player.Create("Mahmoud", "Gouad", PlayerPosition.Goalkeeper, new Price(4.5m), masry.Id),
                    Player.Create("Baher", "El Mohamady", PlayerPosition.Defender, new Price(4.5m), masry.Id),
                    Player.Create("Mido", "Gaber", PlayerPosition.Midfielder, new Price(5.5m), masry.Id),
                    Player.Create("Salah", "Mohsen", PlayerPosition.Forward, new Price(6.0m), masry.Id),
                };

                // Add fake bench fillers so users can form a full 15-man squad
                for(int i = 1; i <= 5; i++)
                {
                    players.Add(Player.Create("Ahly", $"BenchDef{i}", PlayerPosition.Defender, new Price(4.0m), ahly.Id));
                    players.Add(Player.Create("Zamalek", $"BenchMid{i}", PlayerPosition.Midfielder, new Price(4.5m), zamalek.Id));
                }

                context.Players.AddRange(players);

                // 3. Seed initial Gameweeks
                var gw1 = Gameweek.Create(1, DateTime.UtcNow.AddDays(7));
                var gw2 = Gameweek.Create(2, DateTime.UtcNow.AddDays(14));
                context.Gameweeks.AddRange(gw1, gw2);

                await context.SaveChangesAsync();
                logger.LogInformation("Database seeded successfully!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
