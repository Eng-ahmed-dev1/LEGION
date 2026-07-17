namespace FantasyFootball.Application.UseCases.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.RegisterAsync(request.Email, request.Password, request.TeamName, request.Username);
        
        if (!success)
        {
            var errorMessage = errors != null && errors.Any() ? string.Join(", ", errors) : "Registration failed.";
            return Result<Unit>.Failure(new Error("Authentication.RegisterFailed", errorMessage));
        }

        return Result<Unit>.Success(Unit.Value);
    }
}
