namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
          => builder.HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedById)
                   .OnDelete(DeleteBehavior.NoAction);

    }
}
