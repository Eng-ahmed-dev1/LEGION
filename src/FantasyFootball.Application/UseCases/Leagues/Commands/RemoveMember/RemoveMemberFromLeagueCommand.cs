namespace FantasyFootball.Application.UseCases.Leagues.Commands.RemoveMember
{
    public record RemoveMemberFromLeagueCommand(Guid LeagueId, Guid ManagerId) : IRequest<Result<Unit>>;
}
