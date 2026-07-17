namespace FantasyFootball.Application.UseCases.Authentication.Commands.RevokeToken;

public record RevokeTokenCommand(string Email) : IRequest<Result<bool>>;
