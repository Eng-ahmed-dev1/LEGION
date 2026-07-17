namespace FantasyFootball.Application.DTOs
{
    public record TransferDto(
        Guid Id,
        Guid FantasyTeamId,
        Guid PlayerInId,
        Guid PlayerOutId,
        Guid GameweekId,
        bool IsFree);
}