namespace FantasyFootball.Application.UseCases.FantasyPlayers.Queries.GetAllFantasyPlayers
{
    public record GetAllFantasyPlayersQuery : IRequest<Result<IReadOnlyList<FantasyPlayerDto>>>;
}
