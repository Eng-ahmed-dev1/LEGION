namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.RemoveCaptaincy
{
    public record RemoveCaptaincyCommand(Guid playerId) : IRequest<Result<Unit>>;
}
