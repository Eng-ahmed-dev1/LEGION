namespace FantasyFootball.Application.UseCases.Authentication.Commands.Passwords;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : IRequest<Result<bool>>;
