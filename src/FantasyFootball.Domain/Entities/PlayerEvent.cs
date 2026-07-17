namespace FantasyFootball.Domain.Entities
{
    public class PlayerEvent : BaseEntity
    {
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; } = default!;

        public Guid FixtureId { get; private set; }
        public Fixture Fixture { get; private set; } = default!;

        public EventType EventType { get; private set; }
        public int Points { get; private set; }

        private PlayerEvent() { }

        public static PlayerEvent Create(
            Guid playerId,
            Guid fixtureId,
            EventType eventType,
            int points)
        {
            // Points can be negative for cards, own goals, etc.

            return new PlayerEvent
            {
                PlayerId = playerId,
                FixtureId = fixtureId,
                EventType = eventType,
                Points = points
            };
        }

        public void ChangeEvent(EventType eventType, int points)
        {
            // Points can be negative

            EventType = eventType;
            Points = points;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddBonusPoints(int bonusPoints)
        {
            if (bonusPoints <= 0)
                throw new DomainException("Bonus points must be greater than zero.");

            Points += bonusPoints;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeductPoints(int pointsToDeduct)
        {
            if (pointsToDeduct <= 0)
                throw new DomainException("Points to deduct must be greater than zero.");

            Points -= pointsToDeduct;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsGoal() => EventType == EventType.Goal;

        public bool IsAssist() => EventType == EventType.Assist;

        public bool IsCleanSheet() => EventType == EventType.CleanSheet;


    }
}