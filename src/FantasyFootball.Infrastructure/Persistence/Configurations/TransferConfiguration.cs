namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasOne(x => x.PlayerIn)
                .WithMany()
                .HasForeignKey(x => x.PlayerInId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PlayerOut)
                .WithMany()
                .HasForeignKey(x => x.PlayerOutId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
