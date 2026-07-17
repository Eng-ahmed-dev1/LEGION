namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Players;

public class ApiPlayerTeamDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
