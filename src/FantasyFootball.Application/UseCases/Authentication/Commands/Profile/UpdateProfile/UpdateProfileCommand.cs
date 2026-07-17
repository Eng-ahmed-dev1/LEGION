namespace FantasyFootball.Application.UseCases.Authentication.Commands.Profile;

public record UpdateProfileCommand(Guid UserId, string Email, string UserName) : IRequest<Result<bool>>;
