namespace FantasyFootball.Infrastructure.Repositories
{
    public class TransferRepository : Repository<Transfer>, ITransferRepository
    {
        public TransferRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Transfer>> GetByFantasyTeamIdAsync(Guid fantasyTeamId)
            => await _dbSet.AsNoTracking().Where(x => x.FantasyTeamId == fantasyTeamId).ToListAsync();

        public async Task<IReadOnlyList<Transfer>> GetByGameweekIdAsync(Guid gameweekId)
            => await _dbSet.AsNoTracking().Where(x => x.GameweekId == gameweekId).ToListAsync();
    }
}
