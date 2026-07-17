namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.UpdateFantasyTeam
{
    public record UpdateFantasyTeamCommand(
        Guid Id,
        string Name,
        decimal Budget,
        int FreeTransfers) : IRequest<Result<Unit>>;
}
