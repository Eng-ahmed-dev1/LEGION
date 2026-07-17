namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures;

public class ApiFixturePlayersResponseDto
{
    [JsonPropertyName("team")]
    public ApiTeamDto Team { get; set; } = new();

    [JsonPropertyName("players")]
    public List<ApiFixturePlayerDto> Players { get; set; } = new();
}
