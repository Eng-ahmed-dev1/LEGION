namespace FantasyFootball.Application.UseCases.Gameweeks.Queries.GetGameweekById
{
    public record GetGameweekByIdQuery(Guid Id) : IRequest<Result<GameweekDto?>>;
}
