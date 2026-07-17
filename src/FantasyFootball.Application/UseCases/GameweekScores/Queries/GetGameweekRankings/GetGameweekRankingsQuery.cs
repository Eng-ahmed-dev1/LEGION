namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekRankings
{
    public record GetGameweekRankingsQuery(Guid GameweekId) : IRequest<Result<IReadOnlyList<GameweekScoreDto>>>;
}
