namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.RemoveCaptaincy
{
    public class RemoveCaptaincyCommandHandler : IRequestHandler<RemoveCaptaincyCommand, Result<Unit>>
    {
        private readonly IFantasyPlayerRepository _FantasyPlayerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCaptaincyCommandHandler(IFantasyPlayerRepository fantasyPlayerRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _FantasyPlayerRepository = fantasyPlayerRepository;
        }
        public async Task<Result<Unit>> Handle(RemoveCaptaincyCommand request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = await _FantasyPlayerRepository.GetByIdAsync(request.playerId);
            if (fantasyPlayer is null)
                return Result<Unit>.Failure(new Error("Not Found", "This player not found"));

            try
            {
                fantasyPlayer.RemoveCaptaincy();
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("DomainError", ex.Message));
            }
            _FantasyPlayerRepository.Update(fantasyPlayer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}