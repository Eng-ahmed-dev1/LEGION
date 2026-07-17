namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.UpdateGameweek
{
    public class UpdateGameweekCommandHandler : IRequestHandler<UpdateGameweekCommand, Result<MediatR.Unit>>
    {
        private readonly IGameweekRepository _gameweekRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGameweekCommandHandler(IGameweekRepository gameweekRepository, IUnitOfWork unitOfWork)
        {
            _gameweekRepository = gameweekRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateGameweekCommand request, CancellationToken cancellationToken)
        {
            var gameweek = await _gameweekRepository.GetByIdAsync(request.Id);

            if (gameweek is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));


            try
            {
                gameweek.RescheduleDeadline(request.Deadline);
                if (request.IsActive) gameweek.Activate();
                if (request.IsFinished) gameweek.Finish();

                _gameweekRepository.Update(gameweek);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DomainException ex)
            {
                return Result<MediatR.Unit>.Failure(new Error("Domain.Error", ex.Message));
            }

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
