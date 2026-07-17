namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.CreateLeagueMember
{
    public class CreateLeagueMemberCommandHandler : IRequestHandler<CreateLeagueMemberCommand, Result<Guid>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeagueMemberCommandHandler(ILeagueMemberRepository leagueMemberRepository, IUnitOfWork unitOfWork)
        {
            _leagueMemberRepository = leagueMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateLeagueMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var leagueMember = LeagueMember.Create(request.LeagueId, request.ManagerId);

                await _leagueMemberRepository.AddAsync(leagueMember);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(leagueMember.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
