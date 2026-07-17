namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.UpdateGameweekScore
{
    public record UpdateGameweekScoreCommand(
        Guid Id,
        int Points,
        Guid ManagerId,
        Guid GameweekId) : IRequest<Result<MediatR.Unit>>;
}
