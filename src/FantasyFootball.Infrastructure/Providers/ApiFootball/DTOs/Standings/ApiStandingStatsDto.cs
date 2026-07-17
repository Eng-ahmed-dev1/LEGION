namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings;

public class ApiStandingStatsDto
{
    [JsonPropertyName("played")]
    public int Played { get; set; }

    [JsonPropertyName("win")]
    public int Win { get; set; }

    [JsonPropertyName("draw")]
    public int Draw { get; set; }

    [JsonPropertyName("lose")]
    public int Lose { get; set; }

    [JsonPropertyName("goals")]
    public ApiStandingGoalsDto Goals { get; set; } = new();
}
