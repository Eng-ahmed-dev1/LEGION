namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CalculateGameweekScore
{
    public record CalculateGameweekScoreCommand(Guid ManagerId, Guid GameweekId) : IRequest<Result<int>>;
}
