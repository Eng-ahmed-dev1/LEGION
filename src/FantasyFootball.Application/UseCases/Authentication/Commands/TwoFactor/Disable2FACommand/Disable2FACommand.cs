namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public record Disable2FACommand(Guid UserId) : IRequest<Result<bool>>;
