namespace FantasyFootball.Application.UseCases.Players.Commands.MarkPlayerAsAvailable
{
    public record MarkPlayerAsAvailableCommand(Guid PlayerId) : IRequest<Result<Unit>>;
}
