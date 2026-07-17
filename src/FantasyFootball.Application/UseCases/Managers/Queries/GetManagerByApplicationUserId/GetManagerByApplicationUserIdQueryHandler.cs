namespace FantasyFootball.Application.UseCases.Managers.Queries.GetManagerByApplicationUserId
{
    public class GetManagerByApplicationUserIdQueryHandler : IRequestHandler<GetManagerByApplicationUserIdQuery, Result<ManagerDto?>>
    {
        private readonly IManagerRepository _managerRepository;

        public GetManagerByApplicationUserIdQueryHandler(IManagerRepository managerRepository)
        => _managerRepository = managerRepository;

        public async Task<Result<ManagerDto?>> Handle(GetManagerByApplicationUserIdQuery request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByApplicationUserIdAsync(request.ApplicationUserId);
            return Result<ManagerDto?>.Success(manager.Adapt<ManagerDto?>());
        }
    }
}
