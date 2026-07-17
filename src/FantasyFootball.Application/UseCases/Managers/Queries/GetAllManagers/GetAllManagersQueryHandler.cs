namespace FantasyFootball.Application.UseCases.Managers.Queries.GetAllManagers
{
    public class GetAllManagersQueryHandler : IRequestHandler<GetAllManagersQuery, Result<IReadOnlyList<ManagerDto>>>
    {
        private readonly IManagerRepository _managerRepository;

        public GetAllManagersQueryHandler(IManagerRepository managerRepository)
        => _managerRepository = managerRepository;

        public async Task<Result<IReadOnlyList<ManagerDto>>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
        {
            var managers = await _managerRepository.GetAllAsync();
            return Result<IReadOnlyList<ManagerDto>>.Success(managers.Adapt<IReadOnlyList<ManagerDto>>());
        }
    }
}
