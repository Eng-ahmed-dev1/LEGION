namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.Finish
{
    public class FinishGameweekCommandHandler : IRequestHandler<FinishGameweekCommand, Result<Unit>>
    {
        private readonly IGameweekRepository _gameweekRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FinishGameweekCommandHandler(IGameweekRepository gameweekRepository, IUnitOfWork unitOfWork)
        {
            _gameweekRepository = gameweekRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(FinishGameweekCommand request, CancellationToken cancellationToken)
        {
            var gameweek = await _gameweekRepository.GetByIdAsync(request.GameweekId);

            if (gameweek is null)
                return Result<Unit>.Failure(new Error("Not.Found", "Gameweek not found"));

            try
            {
                gameweek.Finish();

                _gameweekRepository.Update(gameweek);
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
