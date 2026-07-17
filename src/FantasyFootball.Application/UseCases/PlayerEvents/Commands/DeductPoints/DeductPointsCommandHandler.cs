namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.DeductPoints
{
    public class DeductPointsCommandHandler : IRequestHandler<DeductPointsCommand, Result<Unit>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeductPointsCommandHandler(IPlayerEventRepository playerEventRepository, IUnitOfWork unitOfWork)
        {
            _playerEventRepository = playerEventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeductPointsCommand request, CancellationToken cancellationToken)
        {
            var playerEvent = await _playerEventRepository.GetByIdAsync(request.PlayerEventId);

            if (playerEvent is null)
                return Result<Unit>.Failure(new Error("Not.Found", "PlayerEvent not found"));

            try
            {
                playerEvent.DeductPoints(request.PointsToDeduct);

                _playerEventRepository.Update(playerEvent);
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
