using FantasyFootball.Domain.Enums;

namespace FantasyFootball.Application.UseCases.Players.Queries.GetByPosition
{
    public record GetByPositionQuery(PlayerPosition Position) : IRequest<Result<IReadOnlyList<PlayerDto>>>;
}
