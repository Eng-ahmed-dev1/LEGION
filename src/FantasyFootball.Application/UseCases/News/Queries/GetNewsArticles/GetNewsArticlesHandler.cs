namespace FantasyFootball.Application.UseCases.News.Queries.GetNewsArticles;


public class GetNewsArticlesQueryHandler : IRequestHandler<GetNewsArticlesQuery, Result<IEnumerable<NewsArticleDto>>>
{
    private readonly INewsArticleRepository _repository;

    public GetNewsArticlesQueryHandler(INewsArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<NewsArticleDto>>> Handle(GetNewsArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _repository.GetAllPublishedAsync();
        var dtos = articles.Select(a => new NewsArticleDto(
            a.Id, a.Title, a.Slug, a.Summary, a.Content, a.Category, a.ImageUrl, a.CreatedAt, a.PublishedAt
        ));

        return Result<IEnumerable<NewsArticleDto>>.Success(dtos);
    }
}
