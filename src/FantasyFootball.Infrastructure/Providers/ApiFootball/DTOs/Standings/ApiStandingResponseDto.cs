namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Standings;

public class ApiStandingResponseDto
{
    [JsonPropertyName("league")]
    public ApiStandingLeagueDto League { get; set; } = new();
}
