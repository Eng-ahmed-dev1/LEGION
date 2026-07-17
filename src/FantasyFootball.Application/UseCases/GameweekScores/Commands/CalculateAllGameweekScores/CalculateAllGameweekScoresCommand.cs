namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateAllGameweekScores
{
    public record CalculateAllGameweekScoresCommand(Guid GameweekId) : IRequest<Result<Unit>>;
}
