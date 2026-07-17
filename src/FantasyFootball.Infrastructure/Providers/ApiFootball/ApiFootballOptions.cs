namespace FantasyFootball.Infrastructure.Providers.ApiFootball;

public class ApiFootballOptions
{
    public const string SectionName = "ApiFootball";
    
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiHost { get; set; } = string.Empty;
    public int LeagueId { get; set; } // Default league to sync (e.g., 233 for Egypt)
    public int Season { get; set; } // e.g., 2023
}
