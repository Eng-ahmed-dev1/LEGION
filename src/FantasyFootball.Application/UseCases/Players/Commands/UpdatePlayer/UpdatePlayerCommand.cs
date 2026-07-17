namespace FantasyFootball.Application.UseCases.Players.Commands.UpdatePlayer
{
    public record UpdatePlayerCommand(
     Guid Id,
     string FirstName,
     string LastName,
     decimal Price) : IRequest<Result<MediatR.Unit>>;
}
