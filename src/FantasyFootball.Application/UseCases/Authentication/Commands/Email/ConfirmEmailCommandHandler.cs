namespace FantasyFootball.Application.UseCases.Authentication.Commands.Email;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public ConfirmEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.ConfirmEmailAsync(request.Email, request.Token);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.ConfirmEmailFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
