namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetAllGameweekScores
{
    public record GetAllGameweekScoresQuery : IRequest<Result<IReadOnlyList<GameweekScoreDto>>>;
}
