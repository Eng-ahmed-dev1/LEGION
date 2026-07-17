namespace FantasyFootball.Domain.Entities;

public class DataSyncHistory : BaseEntity
{
    public string Provider { get; private set; } = default!;
    public string SyncType { get; private set; } = default!; // e.g. "Players", "Fixtures"
    public Guid CorrelationId { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public long DurationMs { get; private set; }
    public string Status { get; private set; } = default!; // "InProgress", "Success", "Failed"
    public string? Error { get; private set; }
    public int RecordsInserted { get; private set; }
    public int RecordsUpdated { get; private set; }

    private DataSyncHistory() { }

    public static DataSyncHistory Start(string provider, string syncType, Guid? correlationId = null)
    {
        return new DataSyncHistory
        {
            Provider = provider,
            SyncType = syncType,
            CorrelationId = correlationId ?? Guid.NewGuid(),
            StartedAt = DateTime.UtcNow,
            Status = "InProgress",
            RecordsInserted = 0,
            RecordsUpdated = 0
        };
    }

    public void Complete(int inserted, int updated)
    {
        FinishedAt = DateTime.UtcNow;
        DurationMs = (long)(FinishedAt.Value - StartedAt).TotalMilliseconds;
        RecordsInserted = inserted;
        RecordsUpdated = updated;
        Status = "Success";
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail(string error)
    {
        FinishedAt = DateTime.UtcNow;
        DurationMs = (long)(FinishedAt.Value - StartedAt).TotalMilliseconds;
        Error = error;
        Status = "Failed";
        UpdatedAt = DateTime.UtcNow;
    }
}
