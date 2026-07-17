namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings;

public class ApiStandingGoalsDto
{
    [JsonPropertyName("for")]
    public int For { get; set; }

    [JsonPropertyName("against")]
    public int Against { get; set; }
}
