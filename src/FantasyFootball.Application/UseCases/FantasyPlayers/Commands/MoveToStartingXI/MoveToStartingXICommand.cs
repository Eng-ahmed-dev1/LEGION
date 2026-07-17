namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.MoveToStartingXI
{
    public record MoveToStartingXICommand(Guid PlayerId) : IRequest<Result<Unit>>;
}
