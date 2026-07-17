public class LeagueMember : BaseEntity
{
    public Guid LeagueId { get; private set; }
    public League League { get; private set; } = default!;

    public Guid ManagerId { get; private set; }
    public Manager Manager { get; private set; } = default!;

    public DateTime JoinedAt { get; private set; }
    public int TotalPoints { get; private set; }

    private LeagueMember() { }

    public static LeagueMember Create(Guid leagueId, Guid managerId)
    {
        if (leagueId == Guid.Empty)
            throw new DomainException("League is required");

        if (managerId == Guid.Empty)
            throw new DomainException("Manager is required");

        return new LeagueMember
        {
            LeagueId = leagueId,
            ManagerId = managerId,
            JoinedAt = DateTime.UtcNow,
            TotalPoints = 0
        };
    }

    public void AddPoints(int points)
    {
        if (points < 0)
            throw new DomainException("Points cannot be negative");

        TotalPoints += points;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CorrectPoints(int points)
    {
        if (points < 0)
            throw new DomainException("Points cannot be negative");

        TotalPoints = points;
        UpdatedAt = DateTime.UtcNow;
    }
}