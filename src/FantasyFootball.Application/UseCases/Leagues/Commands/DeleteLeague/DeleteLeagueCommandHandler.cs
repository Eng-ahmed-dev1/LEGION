namespace FantasyFootball.Application.UseCases.Leagues.Commands.DeleteLeague
{
    public class DeleteLeagueCommandHandler : IRequestHandler<DeleteLeagueCommand, Result<MediatR.Unit>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeagueCommandHandler(ILeagueRepository leagueRepository, IUnitOfWork unitOfWork)
        {
            _leagueRepository = leagueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteLeagueCommand request, CancellationToken cancellationToken)
        {
            var league = await _leagueRepository.GetByIdAsync(request.Id);

            if (league is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _leagueRepository.Delete(league);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
