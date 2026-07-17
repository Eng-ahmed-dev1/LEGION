namespace FantasyFootball.Infrastructure.Persistence.Configurations;

public class PlayerNewsConfiguration : IEntityTypeConfiguration<PlayerNews>
{
    public void Configure(EntityTypeBuilder<PlayerNews> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NewsText)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(x => x.Player)
            .WithMany(p => p.News)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.ExpiresAt);
    }
}
