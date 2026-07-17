namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public class Enable2FACommandHandler : IRequestHandler<Enable2FACommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public Enable2FACommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(Enable2FACommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.Enable2FAAsync(request.UserId);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.Enable2FAFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
