namespace FantasyFootball.Application.UseCases.News.Queries.GetNewsArticleById;


public class GetNewsArticleByIdQueryHandler : IRequestHandler<GetNewsArticleByIdQuery, Result<NewsArticleDto>>
{
    private readonly INewsArticleRepository _repository;

    public GetNewsArticleByIdQueryHandler(INewsArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<NewsArticleDto>> Handle(GetNewsArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(request.Id);

        if (article == null || !article.IsPublished)
            return Result<NewsArticleDto>.Failure(new Error("News.NotFound", "Article not found."));

        var dto = new NewsArticleDto(article.Id, article.Title, article.Slug, article.Summary, article.Content, article.Category, article.ImageUrl, article.CreatedAt, article.PublishedAt);
        return Result<NewsArticleDto>.Success(dto);
    }
}
