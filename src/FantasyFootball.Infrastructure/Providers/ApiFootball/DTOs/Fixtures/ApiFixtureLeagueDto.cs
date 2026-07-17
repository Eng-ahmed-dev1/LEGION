namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures;

public class ApiFixtureLeagueDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("season")]
    public int Season { get; set; }

    [JsonPropertyName("round")]
    public string Round { get; set; } = string.Empty;
}
