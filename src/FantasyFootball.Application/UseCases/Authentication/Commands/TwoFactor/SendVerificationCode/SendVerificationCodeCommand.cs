namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public record SendVerificationCodeCommand(string Email) : IRequest<Result<bool>>;
