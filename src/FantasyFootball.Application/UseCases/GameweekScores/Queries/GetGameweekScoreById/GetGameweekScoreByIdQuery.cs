namespace FantasyFootball.Application.UseCases.GameweekScores.Queries.GetGameweekScoreById
{
    public record GetGameweekScoreByIdQuery(Guid Id) : IRequest<Result<GameweekScoreDto?>>;
}
