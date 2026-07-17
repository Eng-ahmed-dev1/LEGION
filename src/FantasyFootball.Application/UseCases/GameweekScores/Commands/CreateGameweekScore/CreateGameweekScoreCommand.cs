namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.CreateGameweekScore
{
    public record CreateGameweekScoreCommand(Guid ManagerId, Guid GameweekId) : IRequest<Result<Guid>>;
}
