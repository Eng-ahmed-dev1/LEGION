namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetByGameweekId
{
    public class GetByGameweekIdHandler : IRequestHandler<GetByGameweekIdQuery, Result<IReadOnlyList<FixtureDto>>>
    {
        private readonly IFixtureRepository _repo;
        public GetByGameweekIdHandler(IFixtureRepository repo)
        => _repo = repo;
        public async Task<Result<IReadOnlyList<FixtureDto>>> Handle(GetByGameweekIdQuery request, CancellationToken cancellationToken)
        {
            var fixtures = await _repo.GetByGameweekIdAsync(request.Id);
            return Result<IReadOnlyList<FixtureDto>>.Success(fixtures.Adapt<IReadOnlyList<FixtureDto>>());
        }
    }
}
