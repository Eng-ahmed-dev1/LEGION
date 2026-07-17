namespace FantasyFootball.Application.UseCases.GameweekScores.Commands.DeleteGameweekScore
{
    public record DeleteGameweekScoreCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
