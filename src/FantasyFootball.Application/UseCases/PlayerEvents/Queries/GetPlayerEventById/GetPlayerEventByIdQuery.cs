namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventById
{
    public record GetPlayerEventByIdQuery(Guid Id) : IRequest<Result<PlayerEventDto?>>;
}
