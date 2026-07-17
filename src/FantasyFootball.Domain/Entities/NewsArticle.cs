namespace FantasyFootball.Domain.Entities;

public class NewsArticle : BaseEntity
{
    public string Title { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string Summary { get; private set; } = string.Empty;
    public string Content { get; private set; } = default!;
    public string ImageUrl { get; private set; } = string.Empty;
    public string Category { get; private set; } = default!; // e.g., "Tips", "Injuries", "Announcements"
    public bool IsPublished { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    private NewsArticle() { }

    public static NewsArticle Create(string title, string slug, string summary, string content, string category, string imageUrl = "")
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title is required.");
            
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Content is required.");

        return new NewsArticle
        {
            Title = title,
            Slug = slug,
            Summary = summary,
            Content = content,
            Category = category,
            ImageUrl = imageUrl,
            IsPublished = true,
            PublishedAt = DateTime.UtcNow
        };
    }

    public void Update(string title, string slug, string summary, string content, string category, string imageUrl)
    {
        Title = title;
        Slug = slug;
        Summary = summary;
        Content = content;
        Category = category;
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unpublish()
    {
        IsPublished = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        if (!IsPublished)
        {
            IsPublished = true;
            PublishedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
