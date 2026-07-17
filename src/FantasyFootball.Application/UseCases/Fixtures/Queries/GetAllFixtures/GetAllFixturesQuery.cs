namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetAllFixtures
{
    public record GetAllFixturesQuery : IRequest<Result<IReadOnlyList<FixtureDto>>>;
}
