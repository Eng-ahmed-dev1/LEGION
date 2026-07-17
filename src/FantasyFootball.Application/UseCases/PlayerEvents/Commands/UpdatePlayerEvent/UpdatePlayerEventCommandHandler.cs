namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.UpdatePlayerEvent
{
    public class UpdatePlayerEventCommandHandler : IRequestHandler<UpdatePlayerEventCommand, Result<MediatR.Unit>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayerEventCommandHandler(IPlayerEventRepository playerEventRepository, IUnitOfWork unitOfWork)
        {
            _playerEventRepository = playerEventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdatePlayerEventCommand request, CancellationToken cancellationToken)
        {
            var playerEvent = await _playerEventRepository.GetByIdAsync(request.Id);

            if (playerEvent is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            try
            {
                playerEvent.ChangeEvent(request.EventType, request.Points);

                _playerEventRepository.Update(playerEvent);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(MediatR.Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<MediatR.Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
