namespace FantasyFootball.Application.Common.Interfaces.Services
{
    public interface IMatchNotificationService
    {
        Task SendMatchUpdateAsync(string message);
        Task SendPersonalNotificationAsync(string userId, string message);
        Task SendMessageDeletedAsync(string roomId, Guid messageId);
        Task SendMessageEditedAsync(string roomId, Guid messageId, string newContent);
    }
}