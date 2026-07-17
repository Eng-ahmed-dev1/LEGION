namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.AddPlayerToFantasyTeam
{
    public record AddPlayerToFantasyTeamCommand(
        Guid FantasyTeamId,
        Guid PlayerId,
        bool IsOnBench) : IRequest<Result<Unit>>;
}
