namespace FantasyFootball.Application.DTOs
{
    public record LeagueDto(
        Guid Id,
        string Name,
        string Code,
        bool IsPublic,
        Guid CreatedById);
}