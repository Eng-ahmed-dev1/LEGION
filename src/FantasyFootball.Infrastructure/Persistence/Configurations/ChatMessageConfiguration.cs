namespace FantasyFootball.Infrastructure.Persistence.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.RoomId)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
