namespace FantasyFootball.Application.UseCases.Managers.Queries.GetManagerById
{
    public record GetManagerByIdQuery(Guid Id) : IRequest<Result<ManagerDto?>>;
}
