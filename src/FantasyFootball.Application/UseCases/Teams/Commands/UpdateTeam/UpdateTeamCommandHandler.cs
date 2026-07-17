namespace FantasyFootball.Application.UseCases.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Result<MediatR.Unit>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeamCommandHandler(ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.Id);

            if (team is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));

            try
            {
                team.Update(request.Name, request.ShortName, string.Empty, string.Empty);

                _teamRepository.Update(team);

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
