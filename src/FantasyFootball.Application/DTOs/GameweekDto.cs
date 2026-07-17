namespace FantasyFootball.Application.DTOs
{
    public record GameweekDto(
        Guid Id,
        int Number,
        DateTime Deadline,
        bool IsActive,
        bool IsFinished);
}