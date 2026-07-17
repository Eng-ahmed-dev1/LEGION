namespace FantasyFootball.Application.UseCases.Gameweeks.Commands.AddFixture
{
    public record AddFixtureToGameweekCommand(
        Guid GameweekId,
        Guid HomeTeamId,
        Guid AwayTeamId,
        DateTime KickOff) : IRequest<Result<Unit>>;
}
