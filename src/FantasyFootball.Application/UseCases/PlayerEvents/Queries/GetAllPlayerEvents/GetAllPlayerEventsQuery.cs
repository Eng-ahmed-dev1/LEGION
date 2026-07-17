namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetAllPlayerEvents
{
    public record GetAllPlayerEventsQuery : IRequest<Result<IReadOnlyList<PlayerEventDto>>>;
}
