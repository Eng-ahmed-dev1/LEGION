using FantasyFootball.Domain.Enums;

namespace FantasyFootball.Application.UseCases.Players.Commands.CreatePlayer
{
    public record CreatePlayerCommand(
        string FirstName,
        string LastName,
        PlayerPosition Position,
        decimal Price,
        Guid TeamId) : IRequest<Result<Guid>>;
}
