namespace FantasyFootball.Application.Common.Abstractions;

public interface INewsArticleRepository
{
    Task<NewsArticle?> GetByIdAsync(Guid id);
    Task<IEnumerable<NewsArticle>> GetAllPublishedAsync();
    void Add(NewsArticle newsArticle);
    void Update(NewsArticle newsArticle);
    void Delete(NewsArticle newsArticle);
}
