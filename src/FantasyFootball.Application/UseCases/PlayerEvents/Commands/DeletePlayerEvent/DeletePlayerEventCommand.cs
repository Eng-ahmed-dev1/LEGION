namespace FantasyFootball.Application.UseCases.PlayerEvents.Commands.DeletePlayerEvent
{
    public record DeletePlayerEventCommand(Guid Id) : IRequest<Result<MediatR.Unit>>;
}
