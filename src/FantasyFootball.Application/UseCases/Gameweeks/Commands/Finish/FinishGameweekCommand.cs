namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.Finish
{
    public record FinishGameweekCommand(Guid GameweekId) : IRequest<Result<Unit>>;
}
