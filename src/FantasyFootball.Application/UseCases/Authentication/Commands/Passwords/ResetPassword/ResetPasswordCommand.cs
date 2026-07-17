namespace FantasyFootball.Application.UseCases.Authentication.Commands.Passwords;

public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Result<bool>>;
