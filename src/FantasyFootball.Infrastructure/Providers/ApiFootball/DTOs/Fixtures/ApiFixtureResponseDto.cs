namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Fixtures;

public class ApiFixtureResponseDto
{
    [JsonPropertyName("fixture")]
    public ApiFixtureDto Fixture { get; set; } = new();

    [JsonPropertyName("teams")]
    public ApiFixtureTeamsDto Teams { get; set; } = new();

    [JsonPropertyName("goals")]
    public ApiFixtureGoalsDto Goals { get; set; } = new();

    [JsonPropertyName("league")]
    public ApiFixtureLeagueDto League { get; set; } = new();
}
