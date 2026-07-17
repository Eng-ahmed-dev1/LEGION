namespace FantasyFootball.Application.UseCases.Players.Queries.GetPlayerById
{
    public record GetPlayerByIdQuery(Guid Id) : IRequest<Result<PlayerDto>>;
}
