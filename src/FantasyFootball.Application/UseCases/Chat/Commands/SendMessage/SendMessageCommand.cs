namespace FantasyFootball.Application.UseCases.Chat.Commands.SendMessage
{
    public record SendMessageCommand(Guid SenderId, string RoomId, string Content, MessageType Type) : IRequest<Result<Guid>>;
}
