namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetFixtureById
{
    public record GetFixtureByIdQuery(Guid Id) : IRequest<Result<FixtureDto?>>;
}
