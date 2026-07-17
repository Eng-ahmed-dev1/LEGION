namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.AddBonusPoints
{
    public class AddBonusPointsCommandHandler : IRequestHandler<AddBonusPointsCommand, Result<Unit>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddBonusPointsCommandHandler(IPlayerEventRepository playerEventRepository, IUnitOfWork unitOfWork)
        {
            _playerEventRepository = playerEventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AddBonusPointsCommand request, CancellationToken cancellationToken)
        {
            var playerEvent = await _playerEventRepository.GetByIdAsync(request.PlayerEventId);

            if (playerEvent is null)
                return Result<Unit>.Failure(new Error("Not.Found", "PlayerEvent not found"));

            try
            {
                playerEvent.AddBonusPoints(request.BonusPoints);

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
