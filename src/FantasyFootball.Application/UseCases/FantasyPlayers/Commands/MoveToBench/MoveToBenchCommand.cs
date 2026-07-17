namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.MoveToBench
{
    public record MoveToBenchCommand(Guid PlayerId) : IRequest<Result<Unit>>;
}
