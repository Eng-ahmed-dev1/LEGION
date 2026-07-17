namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.DeleteGameweek
{
    public class DeleteGameweekCommandHandler : IRequestHandler<DeleteGameweekCommand, Result<MediatR.Unit>>
    {
        private readonly IGameweekRepository _gameweekRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGameweekCommandHandler(IGameweekRepository gameweekRepository, IUnitOfWork unitOfWork)
        {
            _gameweekRepository = gameweekRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteGameweekCommand request, CancellationToken cancellationToken)
        {
            var gameweek = await _gameweekRepository.GetByIdAsync(request.Id);

            if (gameweek is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _gameweekRepository.Delete(gameweek);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
