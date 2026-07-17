namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Dtos.Teams;

public class ApiTeamResponseDto
{
    [JsonPropertyName("team")]
    public ApiTeamDto Team { get; set; } = new();

    [JsonPropertyName("venue")]
    public ApiVenueDto Venue { get; set; } = new();
}


