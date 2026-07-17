namespace FantasyFootball.Application.UseCases.Authentication.Commands.RevokeToken;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public RevokeTokenCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var success = await _identityService.RevokeTokenAsync(request.Email);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.RevokeTokenFailed", "User not found or revocation failed."));

        return Result<bool>.Success(true);
    }
}
