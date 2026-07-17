namespace FantasyFootball.Application.DTOs
{
    public record AuthResponseDto(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiration);
}
