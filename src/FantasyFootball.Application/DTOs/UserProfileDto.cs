namespace FantasyFootball.Application.DTOs
{
    public record UserProfileDto(Guid UserId, string Email, string UserName, string TeamName);
}
