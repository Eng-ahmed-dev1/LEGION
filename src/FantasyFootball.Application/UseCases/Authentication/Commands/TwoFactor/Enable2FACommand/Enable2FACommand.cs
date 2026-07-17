namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public record Enable2FACommand(Guid UserId) : IRequest<Result<bool>>;
