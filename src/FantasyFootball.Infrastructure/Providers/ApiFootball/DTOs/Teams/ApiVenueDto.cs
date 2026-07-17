namespace FantasyFootball.Infrastructure.Providers.ApiFootball.DTOs.Teams
{
    public class ApiVenueDto
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("surface")]
        public string Surface { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }

}