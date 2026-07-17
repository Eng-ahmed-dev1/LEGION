public class FantasyPlayer : BaseEntity
{
    public bool IsCaptain { get; private set; }
    public bool IsViceCaptain { get; private set; }
    public bool IsOnBench { get; private set; }

    public Guid FantasyTeamId { get; private set; }
    public FantasyTeam FantasyTeam { get; private set; } = default!;

    public Guid PlayerId { get; private set; }
    public Player Player { get; private set; } = default!;

    private FantasyPlayer() { }

    public static FantasyPlayer Create(Guid fantasyTeamId, Guid playerId, bool isOnBench)
    {
        if (fantasyTeamId == Guid.Empty)
            throw new DomainException("FantasyTeam is required");

        if (playerId == Guid.Empty)
            throw new DomainException("Player is required");

        return new FantasyPlayer
        {
            FantasyTeamId = fantasyTeamId,
            PlayerId = playerId,
            IsOnBench = isOnBench,
            IsCaptain = false,
            IsViceCaptain = false
        };
    }

    public void AssignAsCaptain()
    {
        if (IsOnBench)
            throw new DomainException("Bench player cannot be captain");

        IsCaptain = true;
        IsViceCaptain = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignAsViceCaptain()
    {
        if (IsOnBench)
            throw new DomainException("Bench player cannot be vice captain");

        if (IsCaptain)
            throw new DomainException("Captain cannot be vice captain");

        IsViceCaptain = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveCaptaincy()
    {
        IsCaptain = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MoveToStartingXI()
    {
        IsOnBench = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MoveToBench()
    {
        if (IsCaptain)
            throw new DomainException("Cannot bench the captain");

        IsOnBench = true;
        UpdatedAt = DateTime.UtcNow;
    }
}