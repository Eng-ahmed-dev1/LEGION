namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.AddPoints
{
    public record AddPointsToGameweekScoreCommand(Guid GameweekScoreId, int Points) : IRequest<Result<Unit>>;
}
