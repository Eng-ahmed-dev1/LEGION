public class League : BaseEntity
{
    public string Name { get; private set; } = default!;
    public LeagueType Type { get; private set; }

    public Guid CreatedById { get; private set; }
    public Manager CreatedBy { get; private set; } = default!;

    private readonly List<LeagueMember> _members = [];
    public IReadOnlyCollection<LeagueMember> Members => _members.AsReadOnly();

    private League() { }

    public static League Create(
        string name,
        LeagueType type,
        Guid createdById)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");

        if (!Enum.IsDefined(type))
            throw new DomainException("Invalid league type.");

        if (createdById == Guid.Empty)
            throw new DomainException("Creator is required.");

        return new League
        {
            Name = name.Trim(),
            Type = type,
            CreatedById = createdById
        };
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");

        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMember(LeagueMember member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (_members.Any(m => m.ManagerId == member.ManagerId))
            throw new DomainException("Manager is already a member.");

        _members.Add(member);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveMember(Guid managerId)
    {
        if (managerId == CreatedById)
            throw new DomainException("Cannot remove the league creator.");

        var member = _members.FirstOrDefault(m => m.ManagerId == managerId)
            ?? throw new DomainException("Manager is not a member.");

        _members.Remove(member);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsPremium()
        => Type == LeagueType.Premium;

    public bool IsGlobal()
        => Type == LeagueType.Global;
}