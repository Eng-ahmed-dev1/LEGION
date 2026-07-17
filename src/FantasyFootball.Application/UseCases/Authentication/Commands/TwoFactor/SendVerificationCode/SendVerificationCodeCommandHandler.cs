namespace FantasyFootball.Application.UseCases.Authentication.Commands.TwoFactor;

public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public SendVerificationCodeCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SendVerificationCodeAsync(request.Email);
        return Result<bool>.Success(true);
    }
}
