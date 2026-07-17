namespace FantasyFootball.Application.UseCases.Transfers.Commands.CreateTransfer
{
    public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Result<Guid>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IFantasyTeamRepository _fantasyTeamRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameweekRepository _gameweekRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransferCommandHandler(
            ITransferRepository transferRepository,
            IFantasyTeamRepository fantasyTeamRepository,
            IPlayerRepository playerRepository,
            IGameweekRepository gameweekRepository,
            IUnitOfWork unitOfWork)
        {
            _transferRepository = transferRepository;
            _fantasyTeamRepository = fantasyTeamRepository;
            _playerRepository = playerRepository;
            _gameweekRepository = gameweekRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fantasyTeam = await _fantasyTeamRepository.GetByIdWithPlayersAsync(request.FantasyTeamId);
                var playerIn = await _playerRepository.GetByIdAsync(request.PlayerInId);
                var playerOut = await _playerRepository.GetByIdAsync(request.PlayerOutId);
                var gameweek = await _gameweekRepository.GetByIdAsync(request.GameweekId);

                if (fantasyTeam == null || playerIn == null || playerOut == null || gameweek == null)
                    return Result<Guid>.Failure(new Error("Transfer.Invalid", "One or more related entities were not found."));

                var transferService = new FantasyFootball.Domain.Services.TransferDomainService();
                bool isFree = transferService.Execute(fantasyTeam, playerIn, playerOut, gameweek);
                
                var transfer = Transfer.Create(
                    request.FantasyTeamId,
                    request.PlayerInId,
                    request.PlayerOutId,
                    request.GameweekId,
                    isFree);

                await _transferRepository.AddAsync(transfer);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(transfer.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
