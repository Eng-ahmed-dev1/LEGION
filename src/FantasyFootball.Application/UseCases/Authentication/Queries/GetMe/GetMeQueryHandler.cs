namespace FantasyFootball.Application.UseCases.Authentication.Queries.GetMe;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, Result<UserProfileDto>>
{
    private readonly IIdentityService _identityService;

    public GetMeQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<UserProfileDto>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var profile = await _identityService.GetProfileAsync(request.UserId);
        
        if (profile == null)
            return Result<UserProfileDto>.Failure(new Error("Authentication.UserNotFound", "User profile could not be found."));

        return Result<UserProfileDto>.Success(profile);
    }
}
