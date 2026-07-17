namespace FantasyFootball.Application.UseCases.Players.Commands.MarkPlayerAsUnavailable
{
    public record MarkPlayerAsUnavailableCommand(Guid PlayerId, string Reason) : IRequest<Result<Unit>>;
}
