namespace FantasyFootball.Application.UseCases.Leagues.Commands.UpdateLeague
{
    public record UpdateLeagueCommand(
        Guid Id,
        string Name,
        bool IsPublic) : IRequest<Result<MediatR.Unit>>;
}
