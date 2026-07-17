namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoresByManagerId
{
    public record GetGameweekScoresByManagerIdQuery(Guid ManagerId) : IRequest<Result<GameweekScoreDto?>>;
}
