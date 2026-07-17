namespace FantasyFootball.Infrastructure.Repositories;

public class PlayerNewsRepository : IPlayerNewsRepository
{
    private readonly AppDbContext _context;

    public PlayerNewsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PlayerNews>> GetActiveNewsAsync()
    {
        return await _context.PlayerNews
            .Where(x => x.ExpiresAt == null || x.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(x => x.PublishedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<PlayerNews>> GetNewsByPlayerIdAsync(Guid playerId)
    {
        return await _context.PlayerNews
            .Where(x => x.PlayerId == playerId)
            .OrderByDescending(x => x.PublishedAt)
            .ToListAsync();
    }
}
