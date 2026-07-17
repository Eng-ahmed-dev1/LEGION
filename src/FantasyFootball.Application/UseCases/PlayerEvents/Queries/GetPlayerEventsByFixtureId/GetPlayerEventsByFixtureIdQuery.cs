namespace FantasyFootball.Application.UseCases.PlayerEvents.Queries.GetPlayerEventsByFixtureId
{
    public record GetPlayerEventsByFixtureIdQuery(Guid FixtureId) : IRequest<Result<IReadOnlyList<PlayerEventDto>>>;
}
