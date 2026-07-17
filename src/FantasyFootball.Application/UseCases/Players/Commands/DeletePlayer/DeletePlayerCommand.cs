namespace FantasyFootball.Application.UseCases.Players.Commands.DeletePlayer
{
    public record DeletePlayerCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
