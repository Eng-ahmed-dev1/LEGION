namespace FantasyFootball.Application.UseCases.Leagues.Commands.UpdateLeague
{
    public class UpdateLeagueCommandHandler : IRequestHandler<UpdateLeagueCommand, Result<MediatR.Unit>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeagueCommandHandler(ILeagueRepository leagueRepository, IUnitOfWork unitOfWork)
        {
            _leagueRepository = leagueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MediatR.Unit>> Handle(UpdateLeagueCommand request, CancellationToken cancellationToken)
        {
            var league = await _leagueRepository.GetByIdAsync(request.Id);

            if (league is null)
                return Result<MediatR.Unit>.Failure(new Error("Not.Found", "Entity not found"));


            try
            {
                league.Rename(request.Name);
                // Note: IsPublic missing from Domain methods.

                _leagueRepository.Update(league);

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
