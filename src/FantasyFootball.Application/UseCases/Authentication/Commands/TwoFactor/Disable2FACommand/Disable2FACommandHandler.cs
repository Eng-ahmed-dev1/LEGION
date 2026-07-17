namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public class Disable2FACommandHandler : IRequestHandler<Disable2FACommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public Disable2FACommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(Disable2FACommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.Disable2FAAsync(request.UserId);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.Disable2FAFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
