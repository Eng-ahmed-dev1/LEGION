namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.CreateFantasyPlayer
{
    public record CreateFantasyPlayerCommand(Guid FantasyTeamId, Guid PlayerId, bool IsOnBench) : IRequest<Result<Guid>>;
}
