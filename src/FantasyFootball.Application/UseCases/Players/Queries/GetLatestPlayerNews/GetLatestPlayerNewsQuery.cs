namespace FantasyFootball.Application.UseCases.Players.Queries.GetLatestPlayerNews;

public record GetLatestPlayerNewsQuery() : IRequest<Result<IEnumerable<PlayerNewsDto>>>;

public class GetLatestPlayerNewsQueryHandler : IRequestHandler<GetLatestPlayerNewsQuery, Result<IEnumerable<PlayerNewsDto>>>
{
    private readonly IPlayerNewsRepository _repository;

    public GetLatestPlayerNewsQueryHandler(IPlayerNewsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<PlayerNewsDto>>> Handle(GetLatestPlayerNewsQuery request, CancellationToken cancellationToken)
    {
        var news = await _repository.GetActiveNewsAsync();
        var dtos = news.Select(n => new PlayerNewsDto(
            n.Id, n.PlayerId, n.NewsText, n.Type.ToString(), n.ChanceOfPlaying, n.ExpectedReturnDate, n.PublishedAt, n.ExpiresAt, n.Source, n.CreatedAt
        ));
        
        return Result<IEnumerable<PlayerNewsDto>>.Success(dtos);
    }
}
