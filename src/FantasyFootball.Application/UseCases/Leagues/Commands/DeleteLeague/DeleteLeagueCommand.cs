namespace FantasyFootball.Application.UseCases.Leagues.Commands.DeleteLeague
{
    public record DeleteLeagueCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
