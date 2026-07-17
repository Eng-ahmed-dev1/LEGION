namespace FantasyFootball.Application.UseCases.Managers.Queries.GetAllManagers
{
    public record GetAllManagersQuery : IRequest<Result<IReadOnlyList<ManagerDto>>>;
}
