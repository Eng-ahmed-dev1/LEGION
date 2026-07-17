namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.Activate
{
    public record ActivateGameweekCommand(Guid GameweekId) : IRequest<Result<Unit>>;
}
