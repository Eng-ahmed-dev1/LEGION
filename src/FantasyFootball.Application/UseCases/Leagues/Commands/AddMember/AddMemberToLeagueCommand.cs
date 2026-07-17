namespace FantasyFootball.Application.UseCases.Leagues.Commands.AddMember
{
    public record AddMemberToLeagueCommand(Guid LeagueId, Guid ManagerId) : IRequest<Result<Unit>>;
}
