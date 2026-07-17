namespace FantasyFootball.Application.UseCases.Authentication.Commands.Passwords;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public ResetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.ResetPasswordFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
