namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Players;

public class ApiPlayerDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("firstname")]
    public string Firstname { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string Lastname { get; set; } = string.Empty;

    [JsonPropertyName("photo")]
    public string Photo { get; set; } = string.Empty;

    [JsonPropertyName("injured")]
    public bool Injured { get; set; }
}
