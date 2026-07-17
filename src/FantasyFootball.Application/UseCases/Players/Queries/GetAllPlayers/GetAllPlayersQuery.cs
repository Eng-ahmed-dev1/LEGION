namespace FantasyFootball.Application.UseCases.Players.Queries.GetAllPlayers
{

    public record GetAllPlayersQuery(PlayerQueryParameters Parameters) : IRequest<Result<IReadOnlyList<PlayerDto>>>;
}
