namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetActiveGameweek
{
    public record GetActiveGameweekQuery : IRequest<Result<GameweekDto?>>;
}
