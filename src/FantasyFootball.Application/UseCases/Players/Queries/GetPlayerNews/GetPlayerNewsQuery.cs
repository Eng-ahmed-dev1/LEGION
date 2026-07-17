namespace FantasyFootball.Application.UseCases.Players.Queries.GetPlayerNews;

public record GetPlayerNewsQuery(Guid PlayerId) : IRequest<Result<IEnumerable<PlayerNewsDto>>>;

public class GetPlayerNewsQueryHandler : IRequestHandler<GetPlayerNewsQuery, Result<IEnumerable<PlayerNewsDto>>>
{
    private readonly IPlayerNewsRepository _repository;

    public GetPlayerNewsQueryHandler(IPlayerNewsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<PlayerNewsDto>>> Handle(GetPlayerNewsQuery request, CancellationToken cancellationToken)
    {
        var news = await _repository.GetNewsByPlayerIdAsync(request.PlayerId);
        var dtos = news.Select(n => new PlayerNewsDto(
            n.Id, n.PlayerId, n.NewsText, n.Type.ToString(), n.ChanceOfPlaying, n.ExpectedReturnDate, n.PublishedAt, n.ExpiresAt, n.Source, n.CreatedAt
        ));
        
        return Result<IEnumerable<PlayerNewsDto>>.Success(dtos);
    }
}
