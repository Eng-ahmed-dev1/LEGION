namespace FantasyFootball.Infrastructure.Repositories
{
    public class LeagueMemberRepository : Repository<LeagueMember>, ILeagueMemberRepository
    {
        public LeagueMemberRepository(AppDbContext db) : base(db) { }

        public async Task<IReadOnlyList<LeagueMember>> GetByLeagueIdAsync(Guid leagueId)
        => await _dbSet.Where(x => x.LeagueId == leagueId).ToListAsync();

        public async Task<IReadOnlyList<LeagueMember>> GetByManagerIdAsync(Guid managerId)
        => await _dbSet.Where(x => x.ManagerId == managerId).ToListAsync();
    }
}
