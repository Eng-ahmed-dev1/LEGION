namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.DeleteFantasyTeam
{
    public record DeleteFantasyTeamCommand(Guid Id) : IRequest<Result<Unit>>;
}
