namespace FantasyFootball.Infrastructure.Providers.ApiFootball;

public class ApiFootballClient
{
    private readonly HttpClient _httpClient;
    private readonly ApiFootballOptions _options;

    public ApiFootballClient(HttpClient httpClient, IOptions<ApiFootballOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("x-apisports-key", _options.ApiKey);
    }

    public async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(content, options);
    }
}
