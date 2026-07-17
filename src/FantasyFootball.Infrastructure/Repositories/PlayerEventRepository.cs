namespace FantasyFootball.Infrastructure.Repositories
{
    public class PlayerEventRepository : Repository<PlayerEvent>, IPlayerEventRepository
    {
        public PlayerEventRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<PlayerEvent>> GetByPlayerIdAsync(Guid playerId)
            => await _dbSet.AsNoTracking().Where(x => x.PlayerId == playerId).ToListAsync();

        public async Task<IReadOnlyList<PlayerEvent>> GetByFixtureIdAsync(Guid fixtureId)
            => await _dbSet.AsNoTracking().Where(x => x.FixtureId == fixtureId).ToListAsync();
    }
}
