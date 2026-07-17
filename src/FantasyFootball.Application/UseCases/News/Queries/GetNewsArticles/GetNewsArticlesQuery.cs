namespace FantasyFootball.Application.UseCases.News.Queries.GetNewsArticles;

public record GetNewsArticlesQuery() : IRequest<Result<IEnumerable<NewsArticleDto>>>;
