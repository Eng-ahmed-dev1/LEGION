namespace FantasyFootball.Application.UseCases.Fixtures.Commands.CorrectFixtureResult
{
    public record CorrectFixtureResultCommand(Guid Id, int HomeScore, int AwayScore) : IRequest<Result<Unit>>;
}
