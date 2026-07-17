namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures;

public class ApiFixtureDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("status")]
    public ApiFixtureStatusDto Status { get; set; } = new();
}
