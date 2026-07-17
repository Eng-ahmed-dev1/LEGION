public class Fixture : BaseEntity
{
    public Guid HomeTeamId { get; private set; }
    public Team HomeTeam { get; private set; } = default!;

    public Guid AwayTeamId { get; private set; }
    public Team AwayTeam { get; private set; } = default!;

    public Guid GameweekId { get; private set; }
    public Gameweek Gameweek { get; private set; } = default!;

    public DateTime KickOff { get; private set; }
    public int? HomeScore { get; private set; }
    public int? AwayScore { get; private set; }
    public bool IsFinished { get; private set; }
    public int? ExternalId { get; private set; }
    public DateTime? LastSyncedAt { get; private set; }

    private Fixture() { }

    public static Fixture Create(
        Guid homeTeamId,
        Guid awayTeamId,
        Guid gameweekId,
        DateTime kickOff)
    {
        if (homeTeamId == Guid.Empty)
            throw new DomainException("Home team is required");

        if (awayTeamId == Guid.Empty)
            throw new DomainException("Away team is required");

        if (gameweekId == Guid.Empty)
            throw new DomainException("Gameweek is required");

        if (homeTeamId == awayTeamId)
            throw new DomainException("Home team and away team cannot be the same");

        if (kickOff < DateTime.UtcNow)
            throw new DomainException("Kickoff time cannot be in the past");

        return new Fixture
        {
            HomeTeamId = homeTeamId,
            AwayTeamId = awayTeamId,
            GameweekId = gameweekId,
            KickOff = kickOff,
            IsFinished = false
        };
    }

    public void RescheduleKickOff(DateTime newKickOff)
    {
        if (IsFinished)
            throw new DomainException("Cannot reschedule a finished fixture");

        if (newKickOff < DateTime.UtcNow)
            throw new DomainException("Kickoff time cannot be in the past");

        KickOff = newKickOff;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordResult(int homeScore, int awayScore)
    {
        if (IsFinished)
            throw new DomainException("Fixture is already finished");

        if (homeScore < 0 || awayScore < 0)
            throw new DomainException("Score cannot be negative");

        HomeScore = homeScore;
        AwayScore = awayScore;
        IsFinished = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CorrectResult(int homeScore, int awayScore)
    {
        if (!IsFinished)
            throw new DomainException("Cannot correct result of an unfinished fixture");

        if (homeScore < 0 || awayScore < 0)
            throw new DomainException("Score cannot be negative");

        HomeScore = homeScore;
        AwayScore = awayScore;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateSyncData(int externalId)
    {
        ExternalId = externalId;
        LastSyncedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}