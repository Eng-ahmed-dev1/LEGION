namespace FantasyFootball.Application.UseCases.Fixtures.Commands.CreateFixture
{
    public record CreateFixtureCommand(Guid HomeTeamId, Guid AwayTeamId, Guid GameweekId, DateTime KickOff) : IRequest<Result<Guid>>;
}
