namespace FantasyFootball.Infrastructure.Repositories
{
    public class GameweekRepository : Repository<Gameweek>, IGameweekRepository
    {
        public GameweekRepository(AppDbContext db) : base(db) { }

        public async Task<Gameweek?> GetActiveGameweekAsync()
         => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.IsActive);

    }
}
