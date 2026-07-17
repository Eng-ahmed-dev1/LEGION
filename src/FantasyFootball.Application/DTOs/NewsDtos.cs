namespace FantasyFootball.Application.DTOs;

public record NewsArticleDto(
    Guid Id,
    string Title,
    string Slug,
    string Summary,
    string Content,
    string Category,
    string ImageUrl,
    DateTime CreatedAt,
    DateTime? PublishedAt
);

public record PlayerNewsDto(
    Guid Id,
    Guid PlayerId,
    string NewsText,
    string Type,
    int? ChanceOfPlaying,
    DateTime? ExpectedReturnDate,
    DateTime PublishedAt,
    DateTime? ExpiresAt,
    string? Source,
    DateTime CreatedAt
);
