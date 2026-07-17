namespace FantasyFootball.Application.UseCases.Authentication.Commands.Profile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<bool>>
{
    private readonly IIdentityService _identityService;

    public UpdateProfileCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<bool>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var (success, errors) = await _identityService.UpdateProfileAsync(request.UserId, request.Email, request.UserName);

        if (!success)
            return Result<bool>.Failure(new Error("Authentication.UpdateProfileFailed", string.Join(" ", errors)));

        return Result<bool>.Success(true);
    }
}
