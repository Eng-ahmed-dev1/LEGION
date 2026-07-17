namespace FantasyFootball.Application.UseCases.Authentication.Commands.Passwords;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public ChangePasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.ChangePasswordFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
