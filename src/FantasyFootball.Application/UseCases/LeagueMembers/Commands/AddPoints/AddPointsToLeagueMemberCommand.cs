namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.AddPoints
{
    public record AddPointsToLeagueMemberCommand(Guid LeagueMemberId, int Points) : IRequest<Result<Unit>>;
}
