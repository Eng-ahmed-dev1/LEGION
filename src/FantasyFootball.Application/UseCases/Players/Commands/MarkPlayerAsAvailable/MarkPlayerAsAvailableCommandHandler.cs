namespace FantasyFootball.Application.UseCases.Players.Commands.MarkPlayerAsAvailable
{
    public class MarkPlayerAsAvailableCommandHandler : IRequestHandler<MarkPlayerAsAvailableCommand, Result<Unit>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkPlayerAsAvailableCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(MarkPlayerAsAvailableCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.PlayerId);

            if (player is null)
                return Result<Unit>.Failure(new Error("Not.Found", "Player not found"));

            try
            {
                player.UpdateAvailability(FantasyFootball.Domain.Enums.AvailabilityStatus.Available, 100);

                _playerRepository.Update(player);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
