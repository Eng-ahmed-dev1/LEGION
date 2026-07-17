namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Players;

public class ApiPlayerGamesDto
{
    [JsonPropertyName("position")]
    public string Position { get; set; } = string.Empty;

    [JsonPropertyName("minutes")]
    public int? Minutes { get; set; }
}
