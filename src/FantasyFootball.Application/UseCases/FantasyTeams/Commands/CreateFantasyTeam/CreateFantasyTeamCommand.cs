namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.CreateFantasyTeam
{
    public record CreateFantasyTeamCommand(string Name, Guid ManagerId) : IRequest<Result<Guid>>;
}
