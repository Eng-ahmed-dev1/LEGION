namespace FantasyFootball.Application.UseCases.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    private readonly IIdentityService _identityService;

    public RefreshTokenCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var (success, authResponse) = await _identityService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

        if (!success || authResponse == null)
            return Result<AuthResponseDto>.Failure(new Error("Authentication.RefreshTokenFailed", "Invalid or expired refresh token."));

        return Result<AuthResponseDto>.Success(authResponse);
    }
}
