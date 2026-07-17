namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.DeleteFantasyPlayer
{
    public record DeleteFantasyPlayerCommand(Guid Id) : IRequest<Result<Unit>>;
}
