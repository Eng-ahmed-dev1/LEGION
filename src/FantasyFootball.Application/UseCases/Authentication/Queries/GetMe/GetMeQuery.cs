namespace FantasyFootball.Application.UseCases.Authentication.Queries.GetMe;

public record GetMeQuery(Guid UserId) : IRequest<Result<UserProfileDto>>;
