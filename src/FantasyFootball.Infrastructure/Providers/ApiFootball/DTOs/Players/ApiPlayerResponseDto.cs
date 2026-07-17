namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Players;

public class ApiPlayerResponseDto
{
    [JsonPropertyName("player")]
    public ApiPlayerDto Player { get; set; } = new();

    [JsonPropertyName("statistics")]
    public List<ApiPlayerStatisticsDto> Statistics { get; set; } = new();
}
