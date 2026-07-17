namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings;

public class ApiStandingDto
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("team")]
    public ApiTeamDto Team { get; set; } = new();

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("goalsDiff")]
    public int GoalsDiff { get; set; }

    [JsonPropertyName("all")]
    public ApiStandingStatsDto All { get; set; } = new();
}
