namespace FantasyFootball.Application.UseCases.Chat.Commands.EditMessage
{
    public record EditMessageCommand(Guid MessageId, Guid SenderId, string NewContent) : IRequest<Result<Guid>>;
}
