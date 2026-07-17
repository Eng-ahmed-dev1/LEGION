namespace FantasyFootball.Infrastructure.Repositories;

public class NewsArticleRepository : INewsArticleRepository
{
    private readonly AppDbContext _context;

    public NewsArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(NewsArticle newsArticle)
    {
        _context.NewsArticles.Add(newsArticle);
    }

    public void Delete(NewsArticle newsArticle)
    {
        _context.NewsArticles.Remove(newsArticle);
    }

    public async Task<IEnumerable<NewsArticle>> GetAllPublishedAsync()
    {
        return await _context.NewsArticles
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<NewsArticle?> GetByIdAsync(Guid id)
    {
        return await _context.NewsArticles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(NewsArticle newsArticle)
    {
        _context.NewsArticles.Update(newsArticle);
    }
}
