namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Common;

public class ApiFootballResponse<T>
{
    [JsonPropertyName("get")]
    public string Get { get; set; } = string.Empty;

    [JsonPropertyName("parameters")]
    public object Parameters { get; set; } = new();

    [JsonPropertyName("errors")]
    public object Errors { get; set; } = new();

    [JsonPropertyName("results")]
    public int Results { get; set; }

    [JsonPropertyName("response")]
    public List<T> Response { get; set; } = new();

    [JsonPropertyName("paging")]
    public ApiPaging Paging { get; set; } = new();
}
