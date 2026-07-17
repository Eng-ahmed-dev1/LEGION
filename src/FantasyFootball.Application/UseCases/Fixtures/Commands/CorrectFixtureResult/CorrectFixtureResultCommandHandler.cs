namespace FantasyFootball.Application.UseCases.Fixtures.Commands.CorrectFixtureResult
{
    public class CorrectFixtureResultCommandHandler : IRequestHandler<CorrectFixtureResultCommand, Result<Unit>>
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CorrectFixtureResultCommandHandler(IFixtureRepository fixtureRepository, IUnitOfWork unitOfWork)
        {
            _fixtureRepository = fixtureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(CorrectFixtureResultCommand request, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureRepository.GetByIdAsync(request.Id);

            if (fixture is null)
                return Result<Unit>.Failure(new Error("Fixture.NotFound", "Fixture not found"));

            try
            {
                fixture.CorrectResult(request.HomeScore, request.AwayScore);

                _fixtureRepository.Update(fixture);
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
