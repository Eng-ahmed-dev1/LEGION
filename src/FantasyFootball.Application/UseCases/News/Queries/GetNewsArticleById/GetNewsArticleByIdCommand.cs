namespace FantasyFootball.Application.UseCases.News.Queries.GetNewsArticleById;

public record GetNewsArticleByIdQuery(Guid Id) : IRequest<Result<NewsArticleDto>>;
