namespace FantasyFootball.Application.UseCases.Players.Commands.AddPlayerNews;

public record AddPlayerNewsCommand(Guid PlayerId, string NewsText, PlayerNewsType Type, int? ChanceOfPlaying, AvailabilityStatus Status, DateTime? ExpectedReturnDate) : IRequest<Result<Guid>>;

public class AddPlayerNewsCommandHandler : IRequestHandler<AddPlayerNewsCommand, Result<Guid>>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPlayerNewsCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddPlayerNewsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _playerRepository.GetByIdAsync(request.PlayerId);
            if (player == null)
                return Result<Guid>.Failure(new Error("Player.NotFound", "Player not found."));

            var news = PlayerNews.Create(request.PlayerId, request.NewsText, request.Type, request.ChanceOfPlaying, request.ExpectedReturnDate);
            player.AddNews(news);
            
            player.UpdateAvailability(request.Status, request.ChanceOfPlaying ?? 100);

            _playerRepository.Update(player);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(news.Id);
        }
        catch (DomainException ex)
        {
            return Result<Guid>.Failure(new Error("PlayerNews.DomainError", ex.Message));
        }
    }
}
