namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
    {
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.HasOne(f => f.HomeTeam)
                   .WithMany()
                   .HasForeignKey(f => f.HomeTeamId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.AwayTeam)
                   .WithMany()
                   .HasForeignKey(f => f.AwayTeamId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}