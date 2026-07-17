namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Players;

public class ApiPlayerGoalsDto
{
    [JsonPropertyName("conceded")]
    public int? Conceded { get; set; }

    [JsonPropertyName("saves")]
    public int? Saves { get; set; }
}
