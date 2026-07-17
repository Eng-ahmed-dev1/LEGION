namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures;

public class ApiFixtureTeamsDto
{
    [JsonPropertyName("home")]
    public ApiFixtureTeamDto Home { get; set; } = new();

    [JsonPropertyName("away")]
    public ApiFixtureTeamDto Away { get; set; } = new();
}
