namespace FantasyFootball.Infrastructure.Repositories
{
    public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<ChatMessage>> GetByRoomIdAsync(string roomId, int limit = 50)
        {
            return await _dbSet
                .Include(x => x.Sender)
                .Where(x => x.RoomId == roomId)
                .OrderByDescending(x => x.SentAt)
                .Take(limit)
                .ToListAsync();
        }
    }
}
