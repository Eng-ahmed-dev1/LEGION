namespace FantasyFootball.Application.UseCases.Managers.Queries.GetManagerByApplicationUserId
{
    public record GetManagerByApplicationUserIdQuery(Guid ApplicationUserId) : IRequest<Result<ManagerDto?>>;
}
