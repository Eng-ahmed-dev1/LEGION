namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.DeletePlayerEvent
{
    public class DeletePlayerEventCommandHandler : IRequestHandler<DeletePlayerEventCommand, Result<MediatR.Unit>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePlayerEventCommandHandler(IPlayerEventRepository playerEventRepository, IUnitOfWork unitOfWork)
        {
            _playerEventRepository = playerEventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeletePlayerEventCommand request, CancellationToken cancellationToken)
        {
            var playerEvent = await _playerEventRepository.GetByIdAsync(request.Id);

            if (playerEvent is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _playerEventRepository.Delete(playerEvent);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
