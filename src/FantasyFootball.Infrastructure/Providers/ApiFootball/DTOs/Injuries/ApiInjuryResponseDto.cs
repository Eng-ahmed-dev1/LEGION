namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Injuries;

public class ApiInjuryResponseDto
{
    [JsonPropertyName("player")]
    public ApiPlayerDto Player { get; set; } = new();
}
