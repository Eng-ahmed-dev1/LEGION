namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public record Verify2FACommand(string Email, string Code) : IRequest<Result<AuthResponseDto>>;
