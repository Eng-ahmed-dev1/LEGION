namespace FantasyFootball.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string ShortName { get; private set; } = default!;
    public int? ExternalId { get; private set; }
    public DateTime? LastSyncedAt { get; private set; }

    private readonly List<Player> _players = [];
    public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

    private Team() { }

    public string Code { get; private set; } = string.Empty;
    public string LogoUrl { get; private set; } = string.Empty;

    // Standings data
    public int Position { get; private set; }
    public int Played { get; private set; }
    public int Won { get; private set; }
    public int Drawn { get; private set; }
    public int Lost { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }
    public int GoalDifference { get; private set; }
    public int Points { get; private set; }

    public void UpdateStandings(int position, int played, int won, int drawn, int lost, int gf, int ga, int gd, int points)
    {
        Position = position;
        Played = played;
        Won = won;
        Drawn = drawn;
        Lost = lost;
        GoalsFor = gf;
        GoalsAgainst = ga;
        GoalDifference = gd;
        Points = points;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Team Create(string name, string shortName, string code = "", string logoUrl = "")
    {
        Validate(name, shortName);

        return new Team
        {
            Name = name,
            ShortName = shortName,
            Code = code,
            LogoUrl = logoUrl
        };
    }

    public void Update(string name, string shortName, string code, string logoUrl)
    {
        Validate(name, shortName);

        Name = name;
        ShortName = shortName;
        Code = code;
        LogoUrl = logoUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (_players.Any(p => p.Id == player.Id))
            throw new DomainException("Player already belongs to the team.");

        _players.Add(player);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemovePlayer(Guid playerId)
    {
        var player = _players.FirstOrDefault(p => p.Id == playerId)
            ?? throw new DomainException("Player not found.");

        _players.Remove(player);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool HasPlayer(Guid playerId)
    {
        return _players.Any(p => p.Id == playerId);
    }

    public void UpdateSyncData(int externalId)
    {
        ExternalId = externalId;
        LastSyncedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void Validate(string name, string shortName)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Team name is required.");

        if (string.IsNullOrWhiteSpace(shortName))
            throw new DomainException("Short name is required.");
    }
}