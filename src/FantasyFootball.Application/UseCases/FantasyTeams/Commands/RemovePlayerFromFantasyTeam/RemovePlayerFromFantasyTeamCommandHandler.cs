namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.RemovePlayerFromFantasyTeam
{
    public class RemovePlayerFromFantasyTeamCommandHandler : IRequestHandler<RemovePlayerFromFantasyTeamCommand, Result<Unit>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePlayerFromFantasyTeamCommandHandler(
            IFantasyTeamRepository fantasyTeamRepository,
            IPlayerRepository playerRepository,
            IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(RemovePlayerFromFantasyTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _fantasyTeamRepository.GetByIdWithPlayersAsync(request.FantasyTeamId);
            if (team is null)
                return Result<Unit>.Failure(new Error("Not Found", "Fantasy team not found"));

            var player = await _playerRepository.GetByIdAsync(request.PlayerId);
            if (player is null)
                return Result<Unit>.Failure(new Error("Not Found", "Player not found"));

            try
            {
                team.RemovePlayer(request.PlayerId, player);

                _fantasyTeamRepository.Update(team);
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
