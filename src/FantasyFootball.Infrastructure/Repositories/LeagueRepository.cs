namespace FantasyFootball.Infrastructure.Repositories
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        public LeagueRepository(AppDbContext db) : base(db) { }



        public async Task<IReadOnlyList<League>> GetByManagerIdAsync(Guid managerId)
        => await _dbSet.AsNoTracking().Where(x => x.CreatedById == managerId).ToListAsync();
    }
}
