namespace FantasyFootball.Application.UseCases.FantasyPlayers.Commands.CreateFantasyPlayer
{
    public class CreateFantasyPlayerCommandHandler : IRequestHandler<CreateFantasyPlayerCommand, Result<Guid>>
    {
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFantasyPlayerCommandHandler(IFantasyPlayerRepository fantasyPlayerRepository, IUnitOfWork unitOfWork)
        {
            _fantasyPlayerRepository = fantasyPlayerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateFantasyPlayerCommand request, CancellationToken cancellationToken)
        {
            var fantasyPlayer = FantasyPlayer.Create(request.FantasyTeamId, request.PlayerId, request.IsOnBench);

            await _fantasyPlayerRepository.AddAsync(fantasyPlayer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(fantasyPlayer.Id);
        }
    }
}
