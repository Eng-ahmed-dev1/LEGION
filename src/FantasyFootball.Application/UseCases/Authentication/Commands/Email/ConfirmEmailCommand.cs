namespace FantasyFootball.Application.UseCases.Authentication.Commands.Email;

public record ConfirmEmailCommand(string Email, string Token) : IRequest<Result<bool>>;
