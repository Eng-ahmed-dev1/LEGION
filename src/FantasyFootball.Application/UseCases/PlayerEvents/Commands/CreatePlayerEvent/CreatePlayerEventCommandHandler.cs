namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.CreatePlayerEvent
{
    public class CreatePlayerEventCommandHandler : IRequestHandler<CreatePlayerEventCommand, Result<Guid>>
    {
        private readonly IPlayerEventRepository _playerEventRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchNotificationService _notificationService;
        private readonly IMediator _mediator;

        public CreatePlayerEventCommandHandler(
            IPlayerEventRepository playerEventRepository, 
            IUnitOfWork unitOfWork,
            IPlayerRepository playerRepository,
            IMatchNotificationService notificationService,
            IMediator mediator)
        {
            _playerEventRepository = playerEventRepository;
            _unitOfWork = unitOfWork;
            _playerRepository = playerRepository;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreatePlayerEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(request.PlayerId);
                if (player == null)
                    return Result<Guid>.Failure(new Error("Player.NotFound", "Player not found."));

                var playerEvent = PlayerEvent.Create(request.PlayerId, request.FixtureId, request.EventType, request.Points);
                
                var scoringDomainService = new ScoringDomainService();
                int calculatedPoints = scoringDomainService.CalculatePoints(player, new[] { playerEvent });
                

                player.AddPoints(calculatedPoints);
                _playerRepository.Update(player);

                await _playerEventRepository.AddAsync(playerEvent);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new LiveScoreUpdateEvent(request.PlayerId, request.FixtureId, calculatedPoints), cancellationToken);

                if (request.EventType == FantasyFootball.Domain.Enums.EventType.Goal)
                {
                    string message = $"🔥 GOOOOOAL! ⚽ {player.FirstName} {player.LastName} just scored and earned {calculatedPoints} points! 🚀 Open the app NOW to see your new rank! 📈";
                    await _notificationService.SendMatchUpdateAsync(message);
                }

                return Result<Guid>.Success(playerEvent.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
