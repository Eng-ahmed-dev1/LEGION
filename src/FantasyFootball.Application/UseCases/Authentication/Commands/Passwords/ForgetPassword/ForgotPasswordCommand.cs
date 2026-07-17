namespace FantasyFootball.Application.UseCases.Authentication.Commands.Passwords;

public record ForgotPasswordCommand(string Email) : IRequest<Result<bool>>;
