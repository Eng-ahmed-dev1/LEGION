namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(e => e.Value)
                .HasPrecision(18, 2)
                .HasColumnName("Price")
                .IsRequired();

            });
            builder.OwnsOne(p => p.TotalPoints, points =>
            {
                points.Property(t => t.Point)
                      .HasColumnName("TotalPoints")
                      .IsRequired();
            });
        }
    }
}
