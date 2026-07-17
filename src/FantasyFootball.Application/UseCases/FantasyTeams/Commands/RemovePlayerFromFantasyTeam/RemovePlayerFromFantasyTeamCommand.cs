namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RemovePlayerFromFantasyTeam
{
    public record RemovePlayerFromFantasyTeamCommand(
        Guid FantasyTeamId,
        Guid PlayerId) : IRequest<Result<Unit>>;
}
