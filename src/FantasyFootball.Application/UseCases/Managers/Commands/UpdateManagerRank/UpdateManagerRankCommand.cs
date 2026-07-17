namespace FantasyFootball.Application.UseCases.Managers.Commands.UpdateManagerRank
{
    public record UpdateManagerRankCommand(Guid ManagerId, int NewRank) : IRequest<Result<Unit>>;
}
