namespace FantasyFootball.Application.UseCases.Authentication.Commands.Email;

public record ResendConfirmationEmailCommand(string Email) : IRequest<Result<bool>>;
