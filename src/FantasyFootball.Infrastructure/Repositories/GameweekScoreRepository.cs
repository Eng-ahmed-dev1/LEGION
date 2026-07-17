namespace FantasyFootball.Infrastructure.Repositories
{
    public class GameweekScoreRepository : Repository<GameweekScore>, IGameweekScoreRepository
    {
        public GameweekScoreRepository(AppDbContext db) : base(db) { }

        public async Task<GameweekScore?> GetByManagerAndGameweekAsync(Guid managerId, Guid gameweekId)
        => await _dbSet.Where(x => x.ManagerId == managerId && x.GameweekId == gameweekId).FirstOrDefaultAsync();


        public async Task<GameweekScore?> GetByManagerIdAsync(Guid managerId)
        => await _dbSet.Where(x => x.ManagerId == managerId).FirstOrDefaultAsync();

    }
}
