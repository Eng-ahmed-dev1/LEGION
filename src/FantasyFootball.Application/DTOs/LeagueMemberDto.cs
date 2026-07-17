namespace FantasyFootball.Application.DTOs
{
    public record LeagueMemberDto(
        Guid Id,
        Guid LeagueId,
        Guid ManagerId,
        DateTime JoinedAt,
        int TotalPoints);
}