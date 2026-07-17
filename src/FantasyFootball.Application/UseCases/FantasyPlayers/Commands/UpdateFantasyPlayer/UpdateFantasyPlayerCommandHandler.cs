namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.UpdateFantasyPlayer
{
    public class UpdateFantasyPlayerCommandHandler : IRequestHandler<UpdateFantasyPlayerCommand, Result<Unit>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFantasyPlayerCommandHandler(IFantasyPlayerRepository fantasyPlayerRepository, IUnitOfWork unitOfWork)
        {
            _fantasyPlayerRepository = fantasyPlayerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateFantasyPlayerCommand request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = await _fantasyPlayerRepository.GetByIdAsync(request.Id);

            if (fantasyPlayer is null)
                return Result<Unit>.Failure(new Error("Not Found", "This player not found"));

            if (request.IsOnBench)
                fantasyPlayer.MoveToBench();
            else
                fantasyPlayer.MoveToStartingXI();

            if (request.IsCaptain)
                fantasyPlayer.AssignAsCaptain();
            else if (request.IsViceCaptain)
                fantasyPlayer.AssignAsViceCaptain();
            else
                fantasyPlayer.RemoveCaptaincy();

            _fantasyPlayerRepository.Update(fantasyPlayer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
