namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.UpdateFantasyPlayer
{
    public record UpdateFantasyPlayerCommand(
        Guid Id,
        Guid FantasyTeamId,
        Guid PlayerId,
        bool IsOnBench,
        bool IsCaptain,
        bool IsViceCaptain) : IRequest<Result<Unit>>;
}
