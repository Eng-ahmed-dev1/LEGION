namespace FantasyFootball.Domain.Entities;

public class PlayerNews : BaseEntity
{
    public Guid PlayerId { get; private set; }
    public Player Player { get; private set; } = default!;
    
    public string NewsText { get; private set; } = default!;
    public PlayerNewsType Type { get; private set; }
    public int? ChanceOfPlaying { get; private set; }
    public DateTime? ExpectedReturnDate { get; private set; }
    public DateTime PublishedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public int? ExternalId { get; private set; }
    public string? Source { get; private set; }

    private PlayerNews() { }

    public static PlayerNews Create(Guid playerId, string newsText, PlayerNewsType type, int? chanceOfPlaying = null, DateTime? expectedReturnDate = null, int? externalId = null, string? source = null)
    {
        if (string.IsNullOrWhiteSpace(newsText))
            throw new DomainException("News text is required.");

        return new PlayerNews
        {
            PlayerId = playerId,
            NewsText = newsText,
            Type = type,
            ChanceOfPlaying = chanceOfPlaying,
            ExpectedReturnDate = expectedReturnDate,
            PublishedAt = DateTime.UtcNow,
            ExternalId = externalId,
            Source = source
        };
    }
}
