namespace FantasyFootball.Application.UseCases.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, Result<MediatR.Unit>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayerCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id);

            if (player is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            try
            {
                player.Update(request.FirstName, request.LastName, new Price(request.Price));

                _playerRepository.Update(player);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<MediatR.Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
