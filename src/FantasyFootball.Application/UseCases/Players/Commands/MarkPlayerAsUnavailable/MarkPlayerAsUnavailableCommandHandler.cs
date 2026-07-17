namespace FantasyFootball.Application.UseCases.Players.Commands.MarkPlayerAsUnavailable
{
    public class MarkPlayerAsUnavailableCommandHandler : IRequestHandler<MarkPlayerAsUnavailableCommand, Result<Unit>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkPlayerAsUnavailableCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(MarkPlayerAsUnavailableCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.PlayerId);

            if (player is null)
                return Result<Unit>.Failure(new Error("Not.Found", "Player not found"));

            try
            {
                player.UpdateAvailability(FantasyFootball.Domain.Enums.AvailabilityStatus.Unavailable, 0);
                player.AddNews(FantasyFootball.Domain.Entities.PlayerNews.Create(player.Id, request.Reason, FantasyFootball.Domain.Enums.PlayerNewsType.Injury, 0));

                _playerRepository.Update(player);
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
