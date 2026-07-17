namespace FantasyFootball.Application.UseCases.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, Result<MediatR.Unit>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePlayerCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id);

            if (player is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _playerRepository.Delete(player);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
