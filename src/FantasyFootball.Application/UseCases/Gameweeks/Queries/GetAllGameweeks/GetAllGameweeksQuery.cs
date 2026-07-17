namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetAllGameweeks
{
    public record GetAllGameweeksQuery : IRequest<Result<IReadOnlyList<GameweekDto>>>;
}
