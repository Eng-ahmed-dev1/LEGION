namespace FantasyFootball.Application.UseCases.LeagueMembers.Commands.DeleteLeagueMember
{
    public class DeleteLeagueMemberCommandHandler : IRequestHandler<DeleteLeagueMemberCommand, Result<MediatR.Unit>>
    {
        private readonly ILeagueMemberRepository _leagueMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeagueMemberCommandHandler(ILeagueMemberRepository leagueMemberRepository, IUnitOfWork unitOfWork)
        {
            _leagueMemberRepository = leagueMemberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(DeleteLeagueMemberCommand request, CancellationToken cancellationToken)
        {
            var leagueMember = await _leagueMemberRepository.GetByIdAsync(request.Id);

            if (leagueMember is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            _leagueMemberRepository.Delete(leagueMember);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
        }
    }
}
