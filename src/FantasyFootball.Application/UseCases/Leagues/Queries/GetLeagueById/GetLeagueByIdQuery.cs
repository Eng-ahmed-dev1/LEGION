namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetLeagueById
{
    public record GetLeagueByIdQuery(Guid Id) : IRequest<Result<LeagueDto?>>;
}
