namespace FantasyFootball.Application.DTOs
{
    public record GameweekScoreDto(
        Guid Id,
        Guid ManagerId,
        Guid GameweekId,
        int Points);
}