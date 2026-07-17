namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RenameFantasyTeam
{
    public record RenameFantasyTeamCommand(Guid id, string name) : IRequest<Result<Unit>>;
}