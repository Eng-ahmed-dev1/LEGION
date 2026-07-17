namespace FantasyFootball.Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext db) : base(db) { }
        public async Task<Team?> GetByNameAsync(string name)
        => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
    }
}