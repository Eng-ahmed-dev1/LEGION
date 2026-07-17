namespace FantasyFootball.Application.UseCases.Fixtures.Commands.RecordFixtureResult
{
    public record RecordFixtureResultCommand(Guid Id, int HomeScore, int AwayScore) : IRequest<Result<Unit>>;
}
