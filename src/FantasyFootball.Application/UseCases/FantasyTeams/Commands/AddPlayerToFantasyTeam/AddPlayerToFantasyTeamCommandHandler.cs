namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.AddPlayerToFantasyTeam
{
    public class AddPlayerToFantasyTeamCommandHandler : IRequestHandler<AddPlayerToFantasyTeamCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IFantasyPlayerRepository _fantasyPlayerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPlayerToFantasyTeamCommandHandler(
            IFantasyTeamRepository fantasyTeamRepository,
            IPlayerRepository playerRepository,
            IFantasyPlayerRepository fantasyPlayerRepository,
            IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _playerRepository = playerRepository;
            _fantasyPlayerRepository = fantasyPlayerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AddPlayerToFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _fantasyTeamRepository.GetByIdWithPlayersAsync(request.FantasyTeamId);
            if (team is null)
                return Result<Unit>.Failure(new Error("Not Found", "Fantasy team not found"));

            var player = await _playerRepository.GetByIdAsync(request.PlayerId);
            if (player is null)
                return Result<Unit>.Failure(new Error("Not Found", "Player not found"));

            try
            {
                var fantasyPlayer = FantasyPlayer.Create(request.FantasyTeamId, request.PlayerId, request.IsOnBench);
                team.AddPlayer(fantasyPlayer, player);

                await _fantasyPlayerRepository.AddAsync(fantasyPlayer);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain Error", ex.Message));
            }
        }
    }
}
