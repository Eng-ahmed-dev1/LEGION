namespace FantasyFootball.Application.DTOs
{
    public class PlayerQueryParameters
    {
        public string? Search { get; set; }

        public Guid? TeamId { get; set; }

        public string? Position { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? SortBy { get; set; }

        public string? SortDirection { get; set; } = "asc";

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}