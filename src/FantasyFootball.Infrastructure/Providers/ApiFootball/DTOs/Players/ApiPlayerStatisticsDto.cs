namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Players;

public class ApiPlayerStatisticsDto
{
    [JsonPropertyName("team")]
    public ApiPlayerTeamDto Team { get; set; } = new();

    [JsonPropertyName("games")]
    public ApiPlayerGamesDto Games { get; set; } = new();

    [JsonPropertyName("goals")]
    public ApiPlayerGoalsDto Goals { get; set; } = new();
}
