namespace FantasyFootball.Infrastructure.Repositories
{
    public class FantasyTeamRepository : Repository<FantasyTeam>, IFantasyTeamRepository
    {
        public FantasyTeamRepository(AppDbContext db) : base(db) { }

        public async Task<FantasyTeam?> GetByManagerIdAsync(Guid id)
        => await _dbSet
            .Include(t => t.Players)
            .ThenInclude(p => p.Player)
            .Where(m => m.ManagerId == id)
            .FirstOrDefaultAsync();

        public async Task<FantasyTeam?> GetByIdWithPlayersAsync(Guid id)
        {
            return await _dbSet
                .Include(t => t.Players)
                .ThenInclude(p => p.Player)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
