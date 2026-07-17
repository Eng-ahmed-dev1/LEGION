namespace FantasyFootball.Infrastructure.Persistence.Configurations;

public class DataSyncHistoryConfiguration : IEntityTypeConfiguration<DataSyncHistory>
{
    public void Configure(EntityTypeBuilder<DataSyncHistory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Provider)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SyncType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.StartedAt);
        builder.HasIndex(x => x.CorrelationId);
    }
}
