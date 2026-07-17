namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.DeleteFantasyPlayer
{
    public class DeleteFantasyPlayerCommandHandler : IRequestHandler<DeleteFantasyPlayerCommand, Result<Unit>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFantasyPlayerCommandHandler(IFantasyPlayerRepository fantasyPlayerRepository, IUnitOfWork unitOfWork)
        {
            _fantasyPlayerRepository = fantasyPlayerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteFantasyPlayerCommand request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = await _fantasyPlayerRepository.GetByIdAsync(request.Id);

            if (fantasyPlayer is null)
                return Result<Unit>.Failure(new Error("Not found", "This player not found "));
            _fantasyPlayerRepository.Delete(fantasyPlayer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
