namespace FantasyFootball.Application.DTOs
{
    public record ManagerDto(
        Guid Id,
        string TeamName,
        int OverallRank);
}