namespace FantasyFootball.Infrastructure.Persistence.Configurations;

public class NewsArticleConfiguration : IEntityTypeConfiguration<NewsArticle>
{
    public void Configure(EntityTypeBuilder<NewsArticle> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);

        builder.HasIndex(x => x.CreatedAt);
    }
}
