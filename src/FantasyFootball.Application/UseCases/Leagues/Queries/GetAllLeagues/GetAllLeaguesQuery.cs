namespace FantasyFootball.Application.UseCases.Leagues.Queries.GetAllLeagues
{
    public record GetAllLeaguesQuery : IRequest<Result<IReadOnlyList<LeagueDto>>>;
}
