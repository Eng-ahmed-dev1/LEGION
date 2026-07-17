    namespace FantasyFootball.Infrastructure.Persistence.AppDbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<FantasyTeam> FantasyTeams => Set<FantasyTeam>();
        public DbSet<FantasyPlayer> FantasyPlayers => Set<FantasyPlayer>();
        public DbSet<Transfer> Transfers => Set<Transfer>();
        public DbSet<League> Leagues => Set<League>();
        public DbSet<LeagueMember> LeagueMembers => Set<LeagueMember>();
        public DbSet<Gameweek> Gameweeks => Set<Gameweek>();
        public DbSet<Fixture> Fixtures => Set<Fixture>();
        public DbSet<GameweekScore> GameweekScores => Set<GameweekScore>();
        public DbSet<PlayerEvent> PlayerEvents => Set<PlayerEvent>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<FantasyFootball.Domain.Entities.NewsArticle> NewsArticles => Set<FantasyFootball.Domain.Entities.NewsArticle>();
        public DbSet<FantasyFootball.Domain.Entities.PlayerNews> PlayerNews => Set<FantasyFootball.Domain.Entities.PlayerNews>();
        public DbSet<FantasyFootball.Domain.Entities.DataSyncHistory> DataSyncHistories => Set<FantasyFootball.Domain.Entities.DataSyncHistory>();
    }
}
