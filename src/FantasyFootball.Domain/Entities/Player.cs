namespace FantasyFootball.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public PlayerPosition Position { get; private set; }
        public Price Price { get; private set; } = default!;
        public TotalPoints TotalPoints { get; private set; } = default!;
        public AvailabilityStatus Status { get; private set; } = AvailabilityStatus.Available;
        public int ChanceOfPlaying { get; private set; } = 100;
        public string ImageUrl { get; private set; } = default!;
        public int? ExternalId { get; private set; }
        public DateTime? LastSyncedAt { get; private set; }
        public Guid TeamId { get; private set; }
        public Team Team { get; private set; } = default!;

        private readonly List<PlayerNews> _news = new();
        public IReadOnlyCollection<PlayerNews> News => _news.AsReadOnly();

        private Player() { }

        public static Player Create(
            string firstName,
            string lastName,
            PlayerPosition position,
            Price price,
            Guid teamId)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name is required");

            if (!Enum.IsDefined(typeof(PlayerPosition), position))
                throw new DomainException("Invalid position");

            return new Player
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Position = position,
                Price = price,
                TotalPoints = new TotalPoints(0),
                TeamId = teamId,
                ImageUrl = "" // Default to empty string instead of null
            };
        }

        public void Update(string firstName, string lastName, Price price)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name is required");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Price = price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAvailability(AvailabilityStatus status, int chanceOfPlaying)
        {
            Status = status;
            ChanceOfPlaying = chanceOfPlaying;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddNews(PlayerNews newsItem)
        {
            _news.Add(newsItem);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateSyncData(int externalId)
        {
            ExternalId = externalId;
            LastSyncedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddPoints(int points)
        {
            var newTotal = TotalPoints.Point + points;
            // Prevent negative total points if needed by domain rule, or allow it.
            // Since TotalPoints value object throws if < 0, we cap it at 0.
            TotalPoints = new TotalPoints(Math.Max(0, newTotal));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}