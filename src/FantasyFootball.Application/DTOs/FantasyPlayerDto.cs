namespace FantasyFootball.Application.DTOs
{
    public record FantasyPlayerDto(
        Guid Id,
        Guid PlayerId,
        Guid FantasyTeamId,
        bool IsCaptain,
        bool IsViceCaptain,
        bool IsOnBench);
}