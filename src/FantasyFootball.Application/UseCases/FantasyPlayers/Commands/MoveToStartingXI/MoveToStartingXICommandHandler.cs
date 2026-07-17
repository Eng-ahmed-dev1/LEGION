namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.MoveToStartingXI
{
    public class MoveToStartingXICommandHandler : IRequestHandler<MoveToStartingXICommand, Result<Unit>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MoveToStartingXICommandHandler(IFantasyPlayerRepository fantasyPlayerRepository, IUnitOfWork unitOfWork)
        {
            _fantasyPlayerRepository = fantasyPlayerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(MoveToStartingXICommand request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = await _fantasyPlayerRepository.GetByIdAsync(request.PlayerId);

            if (fantasyPlayer is null)
                return Result<Unit>.Failure(new Error("Not Found", "This player not found"));

            try
            {
                fantasyPlayer.MoveToStartingXI();
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("DomainError", ex.Message));
            }

            _fantasyPlayerRepository.Update(fantasyPlayer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
