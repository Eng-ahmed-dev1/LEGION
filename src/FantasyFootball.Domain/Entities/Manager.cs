public class Manager : BaseEntity
{
    public string UserName { get; private set; } = default!;
    public string TeamName { get; private set; } = default!;
    public int OverallRank { get; private set; }
    public bool IsPremium { get; private set; } = false;

    public Guid ApplicationUserId { get; private set; }

    private readonly List<LeagueMember> _leagues = [];
    public IReadOnlyList<LeagueMember> Leagues => _leagues.AsReadOnly();

    private Manager() { }

    public static Manager Create(string teamName, Guid applicationUserId, string userName)
    {
        if (string.IsNullOrWhiteSpace(teamName))
            throw new DomainException("Team name is required");

        if (string.IsNullOrWhiteSpace(userName))
            throw new DomainException("Username is required");

        if (applicationUserId == Guid.Empty)
            throw new DomainException("Application user is required");

        return new Manager
        {
            UserName = userName.Trim(),
            TeamName = teamName.Trim(),
            ApplicationUserId = applicationUserId,
            OverallRank = 0
        };
    }

    public void RenameTeam(string teamName)
    {
        if (string.IsNullOrWhiteSpace(teamName))
            throw new DomainException("Team name is required");

        TeamName = teamName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new DomainException("Username is required");

        UserName = userName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRank(int newRank)
    {
        if (newRank <= 0)
            throw new DomainException("Rank must be greater than 0");

        OverallRank = newRank;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpgradeToPremium()
    {
        IsPremium = true;
        UpdatedAt = DateTime.UtcNow;
    }
}