namespace FantasyFootball.Application.UseCases.Fixtures.Commands.RecordFixtureResult
{
    public class RecordFixtureResultCommandHandler : IRequestHandler<RecordFixtureResultCommand, Result<Unit>>
    {
        private readonly IFixtureRepository _fixtureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecordFixtureResultCommandHandler(IFixtureRepository fixtureRepository, IUnitOfWork unitOfWork)
        {
            _fixtureRepository = fixtureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(RecordFixtureResultCommand request, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureRepository.GetByIdAsync(request.Id);

            if (fixture is null)
                return Result<Unit>.Failure(new Error("Fixture.NotFound", "Fixture not found"));

            try
            {
                fixture.RecordResult(request.HomeScore, request.AwayScore);

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
