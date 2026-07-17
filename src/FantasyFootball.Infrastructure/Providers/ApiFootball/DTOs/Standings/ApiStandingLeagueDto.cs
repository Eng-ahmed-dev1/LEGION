namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings;

public class ApiStandingLeagueDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("standings")]
    public List<List<ApiStandingDto>> Standings { get; set; } = new();
}
