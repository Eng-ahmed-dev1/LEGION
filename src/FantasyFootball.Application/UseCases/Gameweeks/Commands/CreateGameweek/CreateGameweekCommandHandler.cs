namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.CreateGameweek
{
    public class CreateGameweekCommandHandler : IRequestHandler<CreateGameweekCommand, Result<Guid>>
    {
        private readonly IGameweekRepository _gameweekRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameweekCommandHandler(IGameweekRepository gameweekRepository, IUnitOfWork unitOfWork)
        {
            _gameweekRepository = gameweekRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateGameweekCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var gameweek = Gameweek.Create(request.Number, request.Deadline);

                await _gameweekRepository.AddAsync(gameweek);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(gameweek.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
