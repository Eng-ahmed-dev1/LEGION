namespace FantasyFootball.Application.UseCases.Authentication.Commands.Passwords;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public ForgotPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // Even if the email doesn't exist, we return success to prevent email enumeration attacks
        await _identityService.ForgotPasswordAsync(request.Email);
        return Result<bool>.Success(true);
    }
}
