public class Gameweek : BaseEntity
{
    public int Number { get; private set; }
    public DateTime Deadline { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFinished { get; private set; }

    private readonly List<Fixture> _fixtures = [];
    public IReadOnlyCollection<Fixture> Fixtures => _fixtures.AsReadOnly();

    private readonly List<GameweekScore> _scores = [];
    public IReadOnlyCollection<GameweekScore> Scores => _scores.AsReadOnly();

    private Gameweek() { }

    public static Gameweek Create(int number, DateTime deadline)
    {
        if (number <= 0)
            throw new DomainException("Gameweek number must be greater than 0");

        if (deadline < DateTime.UtcNow)
            throw new DomainException("Deadline cannot be in the past");

        return new Gameweek
        {
            Number = number,
            Deadline = deadline,
            IsActive = false,
            IsFinished = false
        };
    }

    public void RescheduleDeadline(DateTime newDeadline)
    {
        if (IsActive)
            throw new DomainException("Cannot reschedule an active gameweek");

        if (IsFinished)
            throw new DomainException("Cannot reschedule a finished gameweek");

        if (newDeadline < DateTime.UtcNow)
            throw new DomainException("Deadline cannot be in the past");

        Deadline = newDeadline;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            throw new DomainException("Gameweek is already active");

        if (IsFinished)
            throw new DomainException("Cannot activate a finished gameweek");

        if (!_fixtures.Any())
            throw new DomainException("Cannot activate a gameweek with no fixtures");

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Finish()
    {
        if (!IsActive)
            throw new DomainException("Cannot finish an inactive gameweek");

        if (IsFinished)
            throw new DomainException("Gameweek is already finished");

        if (_fixtures.Any(f => !f.IsFinished))
            throw new DomainException("Cannot finish gameweek with unfinished fixtures");

        IsActive = false;
        IsFinished = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddFixture(Fixture fixture)
    {
        if (IsActive)
            throw new DomainException("Cannot add fixture to an active gameweek");

        if (IsFinished)
            throw new DomainException("Cannot add fixture to a finished gameweek");

        if (_fixtures.Any(f => f.HomeTeamId == fixture.HomeTeamId
                            && f.AwayTeamId == fixture.AwayTeamId))
            throw new DomainException("Fixture already exists in this gameweek");

        _fixtures.Add(fixture);
        UpdatedAt = DateTime.UtcNow;
    }
}