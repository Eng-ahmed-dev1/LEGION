namespace FantasyFootball.Application.UseCases.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string TeamName,
    string Username) : IRequest<Result<Unit>>;
