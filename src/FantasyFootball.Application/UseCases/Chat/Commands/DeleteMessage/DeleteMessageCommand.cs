namespace FantasyFootball.Application.UseCases.Chat.Commands.DeleteMessage
{
    public record DeleteMessageCommand(Guid MessageId, Guid SenderId) : IRequest<Result<bool>>;
}
