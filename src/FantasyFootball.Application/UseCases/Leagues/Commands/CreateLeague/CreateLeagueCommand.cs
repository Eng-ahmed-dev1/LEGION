namespace FantasyFootball.Application.UseCases.Leagues.Commands.CreateLeague
{
    public record CreateLeagueCommand(string Name, LeagueType type, Guid CreatedById) : IRequest<Result<Guid>>;
}
