namespace FantasyFootball.Infrastructure.Persistence.Configurations
{

    public class LeagueMemberConfigutation : IEntityTypeConfiguration<LeagueMember>
    {
        public void Configure(EntityTypeBuilder<LeagueMember> builder)
        => builder.HasOne(x => x.Manager)
                   .WithMany(x => x.Leagues)
                   .HasForeignKey(x => x.ManagerId)
                   .OnDelete(DeleteBehavior.NoAction);


    }
}
