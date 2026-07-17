namespace FantasyFootball.Infrastructure.Repositories
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        public ManagerRepository(AppDbContext db) : base(db) { }

        public async Task<Manager?> GetByApplicationUserIdAsync(Guid applicationUserId)
        => await _dbSet.FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId);

    }
}
