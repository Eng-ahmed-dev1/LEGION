namespace FantasyFootball.Application.UseCases.Fixtures.Commands.DeleteFixture
{
    public record DeleteFixtureCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
