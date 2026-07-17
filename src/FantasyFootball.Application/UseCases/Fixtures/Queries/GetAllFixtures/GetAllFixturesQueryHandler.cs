namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetAllFixtures
{
    public class GetAllFixturesQueryHandler : IRequestHandler<GetAllFixturesQuery, Result<IReadOnlyList<FixtureDto>>>
    {
        private readonly IFixtureRepository _fixtureRepository;

        public GetAllFixturesQueryHandler(IFixtureRepository fixtureRepository)
        => _fixtureRepository = fixtureRepository;

        public async Task<Result<IReadOnlyList<FixtureDto>>> Handle(GetAllFixturesQuery request, CancellationToken cancellationToken)
        {
            var fixtures = await _fixtureRepository.GetAllAsync();
            return Result<IReadOnlyList<FixtureDto>>.Success(fixtures.Adapt<IReadOnlyList<FixtureDto>>());
        }
    }
}
