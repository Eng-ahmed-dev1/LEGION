namespace FantasyFootball.Application.UseCases.Authentication.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponseDto>>;
