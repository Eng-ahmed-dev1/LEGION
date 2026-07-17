namespace FantasyFootball.Application.UseCases.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Result<Guid>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlayerCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var player = Player.Create(
                    request.FirstName,
                    request.LastName,
                    request.Position,
                    new Price(request.Price),
                    request.TeamId);
                await _playerRepository.AddAsync(player);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(player.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
