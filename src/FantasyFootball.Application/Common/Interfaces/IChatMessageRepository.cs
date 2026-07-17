namespace FantasyFootball.Application.Common.Interfaces
{
    public interface IChatMessageRepository : IRepository<ChatMessage>
    {
        Task<IReadOnlyList<ChatMessage>> GetByRoomIdAsync(string roomId, int limit = 50);
    }
}
