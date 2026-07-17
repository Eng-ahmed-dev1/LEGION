namespace FantasyFootball.Application.UseCases.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (success, authResponse) = await _identityService.LoginAsync(request.Email, request.Password);
        
        if (!success || authResponse == null)
            return Result<AuthResponseDto>.Failure(new Error("Authentication.LoginFailed", "Invalid email or password."));

        return Result<AuthResponseDto>.Success(authResponse);
    }
}
