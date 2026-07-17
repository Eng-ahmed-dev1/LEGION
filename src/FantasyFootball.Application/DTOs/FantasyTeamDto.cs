namespace FantasyFootball.Application.DTOs
{
    public record FantasyTeamDto(
        Guid Id,
        string Name,
        decimal Budget,
        int FreeTransfers,
        Guid ManagerId);
}