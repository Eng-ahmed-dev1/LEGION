namespace FantasyFootball.Application.UseCases.Fixtures.Queries.GetByGameweekId
{
    public record GetByGameweekIdQuery(Guid Id) : IRequest<Result<IReadOnlyList<FixtureDto>>>;
}
