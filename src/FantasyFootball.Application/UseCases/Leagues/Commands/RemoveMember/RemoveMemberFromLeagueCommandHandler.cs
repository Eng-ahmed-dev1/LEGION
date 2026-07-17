namespace FantasyFootball.Application.UseCases.Leagues.Commands.RemoveMember
{
    public class RemoveMemberFromLeagueCommandHandler : IRequestHandler<RemoveMemberFromLeagueCommand, Result<Unit>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMemberFromLeagueCommandHandler(ILeagueRepository leagueRepository, IUnitOfWork unitOfWork)
        {
            _leagueRepository = leagueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(RemoveMemberFromLeagueCommand request, CancellationToken cancellationToken)
        {
            var league = await _leagueRepository.GetByIdAsync(request.LeagueId);

            if (league is null)
                return Result<Unit>.Failure(new Error("Not.Found", "League not found"));

            try
            {
                league.RemoveMember(request.ManagerId);

                _leagueRepository.Update(league);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (DomainException ex)
            {
                return Result<Unit>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
