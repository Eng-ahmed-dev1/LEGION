namespace FantasyFootball.Application.DTOs
{
    public record GlobalLeaderboardDto(
        Guid ManagerId,
        string TeamName,
        string UserName,
        int TotalPoints,
        int OverallRank);
}
