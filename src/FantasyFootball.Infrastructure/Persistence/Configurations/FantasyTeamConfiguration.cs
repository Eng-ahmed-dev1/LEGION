namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class FantasyTeamConfiguration : IEntityTypeConfiguration<FantasyTeam>
    {
        public void Configure(EntityTypeBuilder<FantasyTeam> builder)
        => builder.Property(e => e.Budget)
                .HasPrecision(18, 2)
                .IsRequired();

    }
}
