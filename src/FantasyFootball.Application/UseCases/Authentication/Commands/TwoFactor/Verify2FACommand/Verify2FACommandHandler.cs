namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public class Verify2FACommandHandler : IRequestHandler<Verify2FACommand, Result<AuthResponseDto>>
{
    private readonly IIdentityService _identityService;

    public Verify2FACommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<AuthResponseDto>> Handle(Verify2FACommand request, CancellationToken cancellationToken)
    {
        var (success, authResponse) = await _identityService.Verify2FAAsync(request.Email, request.Code);

        if (!success || authResponse == null)
            return Result<AuthResponseDto>.Failure(new Error("Authentication.Verify2FAFailed", "Invalid 2FA code."));

        return Result<AuthResponseDto>.Success(authResponse);
    }
}
