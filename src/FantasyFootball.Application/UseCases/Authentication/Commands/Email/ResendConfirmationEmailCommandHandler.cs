namespace FantasyFootball.Application.UseCases.Authentication.Commands.Email;

public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public ResendConfirmationEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        await _identityService.ResendConfirmationEmailAsync(request.Email);
        return Result<bool>.Success(true);
    }
}
