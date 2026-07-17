namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Events;

public class ApiEventTimeDto
{
    [JsonPropertyName("elapsed")]
    public int Elapsed { get; set; }

    [JsonPropertyName("extra")]
    public int? Extra { get; set; }
}
