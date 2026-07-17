namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.AddPoints
{
    public class AddPointsToLeagueMemberCommandHandler : IRequestHandler<AddPointsToLeagueMemberCommand, Result<Unit>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPointsToLeagueMemberCommandHandler(ILeagueMemberRepository leagueMemberRepository, IUnitOfWork unitOfWork)
        {
            _leagueMemberRepository = leagueMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AddPointsToLeagueMemberCommand request, CancellationToken cancellationToken)
        {
            var leagueMember = await _leagueMemberRepository.GetByIdAsync(request.LeagueMemberId);

            if (leagueMember is null)
                return Result<Unit>.Failure(new Error("Not.Found", "LeagueMember not found"));

            try
            {
                leagueMember.AddPoints(request.Points);

                _leagueMemberRepository.Update(leagueMember);
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
