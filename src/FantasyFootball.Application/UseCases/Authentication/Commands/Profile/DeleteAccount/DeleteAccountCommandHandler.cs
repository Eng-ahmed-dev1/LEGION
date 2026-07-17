namespace FantasyFootball.Application.UseCases.Authentication.Commands.Profile;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public DeleteAccountCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.DeleteAccountAsync(request.UserId);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.DeleteAccountFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
