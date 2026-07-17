namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.RescheduleDeadline
{
    public class RescheduleDeadlineHandler : IRequestHandler<RescheduleDeadlineCommand, Result<Unit>>
    {
        private readonly IGameweekRepository _gamewekkrepo;
        private readonly IUnitOfWork _unitOfWork;
        public RescheduleDeadlineHandler(IGameweekRepository repo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gamewekkrepo = repo;
        }
        public async Task<Result<Unit>> Handle(RescheduleDeadlineCommand request, CancellationToken cancellationToken)
        {
            var gameweek = await _gamewekkrepo.GetByIdAsync(request.id);
            if (gameweek is null)
                return Result<Unit>.Failure(new Error("gameweek.Not found", "This gameweek not found"));

            try
            {
                gameweek.RescheduleDeadline(request.newDeadline);
                _gamewekkrepo.Update(gameweek);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain Exception", ex.Message));
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}