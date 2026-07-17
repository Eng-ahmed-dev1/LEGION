namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetFixtureById
{
    public class GetFixtureByIdQueryHandler : IRequestHandler<GetFixtureByIdQuery, Result<FixtureDto?>>
    {
        private readonly IFixtureRepository _fixtureRepository;

        public GetFixtureByIdQueryHandler(IFixtureRepository fixtureRepository)
        => _fixtureRepository = fixtureRepository;

        public async Task<Result<FixtureDto?>> Handle(GetFixtureByIdQuery request, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureRepository.GetByIdAsync(request.Id);
            return Result<FixtureDto?>.Success(fixture.Adapt<FixtureDto?>());
        }
    }
}
