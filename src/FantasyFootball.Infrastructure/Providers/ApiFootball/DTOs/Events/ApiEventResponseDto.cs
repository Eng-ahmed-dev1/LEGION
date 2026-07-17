namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Events;

public class ApiEventResponseDto
{
    [JsonPropertyName("time")]
    public ApiEventTimeDto Time { get; set; } = new();

    [JsonPropertyName("team")]
    public ApiTeamDto Team { get; set; } = new();

    [JsonPropertyName("player")]
    public ApiPlayerDto Player { get; set; } = new();

    [JsonPropertyName("assist")]
    public ApiPlayerDto Assist { get; set; } = new();

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("detail")]
    public string Detail { get; set; } = string.Empty;
}
