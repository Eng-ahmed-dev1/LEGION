namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Common;

public class ApiPaging
{
    [JsonPropertyName("current")]
    public int Current { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}
