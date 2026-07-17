public class GameweekScore : BaseEntity
{
    public int Points { get; private set; }

    public Guid ManagerId { get; private set; }
    public Manager Manager { get; private set; } = default!;

    public Guid GameweekId { get; private set; }
    public Gameweek Gameweek { get; private set; } = default!;

    private GameweekScore() { }

    public static GameweekScore Create(Guid managerId, Guid gameweekId)
    {
        if (managerId == Guid.Empty)
            throw new DomainException("Manager is required");

        if (gameweekId == Guid.Empty)
            throw new DomainException("Gameweek is required");

        return new GameweekScore
        {
            ManagerId = managerId,
            GameweekId = gameweekId,
            Points = 0
        };
    }

    public void AddPoints(int points)
    {
        if (points < 0)
            throw new DomainException("Points cannot be negative");

        Points += points;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CorrectPoints(int points)
    {
        if (points < 0)
            throw new DomainException("Points cannot be negative");

        Points = points;
        UpdatedAt = DateTime.UtcNow;
    }
}