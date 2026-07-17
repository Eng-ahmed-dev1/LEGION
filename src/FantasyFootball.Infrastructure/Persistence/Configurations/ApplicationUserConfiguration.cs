namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(m => m.Manager)
                .WithOne()
                .HasForeignKey<Manager>(m => m.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
