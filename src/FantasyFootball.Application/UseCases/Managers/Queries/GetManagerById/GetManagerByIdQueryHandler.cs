namespace FantasyFootball.Application.UseCases.Managers.Queries.GetManagerById
{
    public class GetManagerByIdQueryHandler : IRequestHandler<GetManagerByIdQuery, Result<ManagerDto?>>
    {
        private readonly IManagerRepository _managerRepository;

        public GetManagerByIdQueryHandler(IManagerRepository managerRepository)
        => _managerRepository = managerRepository;

        public async Task<Result<ManagerDto?>> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.Id);
            return Result<ManagerDto?>.Success(manager.Adapt<ManagerDto?>());
        }
    }
}
