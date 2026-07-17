namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures;

public class ApiFixturePlayerDto
{
    [JsonPropertyName("player")]
    public ApiPlayerDto Player { get; set; } = new();

    [JsonPropertyName("statistics")]
    public List<ApiPlayerStatisticsDto> Statistics { get; set; } = new();
}
