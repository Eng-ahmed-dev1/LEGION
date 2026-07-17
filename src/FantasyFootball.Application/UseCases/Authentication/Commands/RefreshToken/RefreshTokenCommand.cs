namespace FantasyFootball.Application.UseCases.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<AuthResponseDto>>;
