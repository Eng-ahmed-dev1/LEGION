namespace FantasyFootball.Infrastructure.Repositories
{
    public class FantasyPlayerRepository : Repository<FantasyPlayer>, IFantasyPlayerRepository
    {
        public FantasyPlayerRepository(AppDbContext db) : base(db) { }

        public async Task<FantasyPlayer?> GetByFantasyTeamIdAsync(Guid fantasyTeamId)
        => await _dbSet.FirstOrDefaultAsync(x => x.FantasyTeamId == fantasyTeamId);

    }
}
