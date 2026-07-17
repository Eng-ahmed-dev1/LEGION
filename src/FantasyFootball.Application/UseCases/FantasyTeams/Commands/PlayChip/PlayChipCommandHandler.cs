namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.PlayChip
{
    public class PlayChipCommandHandler : IRequestHandler<PlayChipCommand, Result<Guid>>
    {
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IGameweekRepository _gameweekRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlayChipCommandHandler(IFantasyTeamRepository fantasyTeamRepository, IGameweekRepository gameweekRepository, IUnitOfWork unitOfWork)
        {
            _fantasyTeamRepository = fantasyTeamRepository;
            _gameweekRepository = gameweekRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(PlayChipCommand request, CancellationToken cancellationToken)
        {
            var team = await _fantasyTeamRepository.GetByIdAsync(request.FantasyTeamId);
            if (team == null) return Result<Guid>.Failure(new Error("FantasyTeamNotFound", "Fantasy Team not found."));

            var gameweek = await _gameweekRepository.GetByIdAsync(request.GameweekId);
            if (gameweek == null) return Result<Guid>.Failure(new Error("GameweekNotFound", "Gameweek not found."));

            if (gameweek.IsFinished) return Result<Guid>.Failure(new Error("GameweekFinished", "Cannot play chip on a finished gameweek."));
            
            try
            {
                team.PlayChip(request.ChipType, request.GameweekId);
                _fantasyTeamRepository.Update(team);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                return Result<Guid>.Success(team.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("DomainError", ex.Message));
            }
        }
    }
}
