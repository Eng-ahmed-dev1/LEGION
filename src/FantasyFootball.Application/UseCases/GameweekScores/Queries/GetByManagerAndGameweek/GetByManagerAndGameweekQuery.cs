namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetByManagerAndGameweek
{
    public record GetByManagerAndGameweekQuery(Guid ManagerId, Guid GameweekId) : IRequest<Result<GameweekScoreDto?>>;
}
