namespace FantasyFootball.Infrastructure.Providers.ApiFootball.DTOs.Teams
{
    public class ApiTeamDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("founded")]
        public int? Founded { get; set; }

        [JsonPropertyName("national")]
        public bool National { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; } = string.Empty;
    }

}