namespace FantasyFootball.Infrastructure.Hubs
{
    public class MatchNotificationService : IMatchNotificationService
    {
        private readonly IHubContext<MatchHub> _hubContext;
        public MatchNotificationService(IHubContext<MatchHub> hubContext)
        => _hubContext = hubContext;
        public async Task SendMatchUpdateAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMatchUpdate", message);
        }

        public async Task SendPersonalNotificationAsync(string userId, string message)
        {
            // Note: SignalR maps JWT Subject/NameIdentifier to User Identifier
            await _hubContext.Clients.User(userId).SendAsync("ReceivePersonalNotification", message);
        }

        public async Task SendMessageDeletedAsync(string roomId, Guid messageId)
        {
            // Ideally we'd send to a Group(roomId), but for MVP we broadcast it
            await _hubContext.Clients.All.SendAsync("ReceiveMessageDeleted", roomId, messageId);
        }

        public async Task SendMessageEditedAsync(string roomId, Guid messageId, string newContent)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessageEdited", roomId, messageId, newContent);
        }
    }
}