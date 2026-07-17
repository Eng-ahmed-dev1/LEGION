namespace FantasyFootball.Application.UseCases.Authentication.Commands.Profile;

public record DeleteAccountCommand(Guid UserId) : IRequest<Result<bool>>;
