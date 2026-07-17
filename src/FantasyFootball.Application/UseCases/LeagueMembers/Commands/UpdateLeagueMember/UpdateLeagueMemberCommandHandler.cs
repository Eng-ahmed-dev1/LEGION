namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.UpdateLeagueMember
{
    public class UpdateLeagueMemberCommandHandler : IRequestHandler<UpdateLeagueMemberCommand, Result<MediatR.Unit>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeagueMemberCommandHandler(ILeagueMemberRepository leagueMemberRepository, IUnitOfWork unitOfWork)
        {
            _leagueMemberRepository = leagueMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateLeagueMemberCommand request, CancellationToken cancellationToken)
        {
            var leagueMember = await _leagueMemberRepository.GetByIdAsync(request.Id);

            if (leagueMember is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            try
            {
                leagueMember.CorrectPoints(request.TotalPoints);

                _leagueMemberRepository.Update(leagueMember);

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
