namespace FantasyFootball.Application.DTOs
{
    public record FixtureDto(
        Guid Id,
        Guid HomeTeamId,
        Guid AwayTeamId,
        Guid GameweekId,
        DateTime KickOff,
        int? HomeScore,
        int? AwayScore,
        bool IsFinished);
}