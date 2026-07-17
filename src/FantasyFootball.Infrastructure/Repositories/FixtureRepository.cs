namespace FantasyFootball.Infrastructure.Repositories
{
    public class FixtureRepository : Repository<Fixture>, IFixtureRepository
    {
        public FixtureRepository(AppDbContext db) : base(db) { }

        public async Task<IReadOnlyList<Fixture>> GetByGameweekIdAsync(Guid gameweekId)
        => await _dbSet.AsNoTracking().Where(x => x.GameweekId == gameweekId).ToListAsync();
    }
}
