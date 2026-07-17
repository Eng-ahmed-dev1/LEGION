namespace FantasyFootball.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public Guid SenderId { get; private set; }
        public Manager Sender { get; private set; } = default!;

        public string RoomId { get; private set; } = default!;

        public string Content { get; private set; } = default!;
        public MessageType Type { get; private set; }
        public DateTime SentAt { get; private set; }
        public bool IsEdited { get; private set; }

        private ChatMessage() { }

        public static ChatMessage Create(Guid senderId, string roomId, string content, MessageType type, bool isSenderPremium)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Message content cannot be empty.");

            if (type == MessageType.Sticker && !isSenderPremium)
                throw new DomainException("Chat.PremiumRequired: Stickers are only available for Premium users.");

            return new ChatMessage
            {
                SenderId = senderId,
                RoomId = roomId,
                Content = content,
                Type = type,
                SentAt = DateTime.UtcNow,
                IsEdited = false
            };
        }

        public void Edit(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new DomainException("Message content cannot be empty.");

            if (Type == MessageType.Sticker)
                throw new DomainException("Chat.CannotEditSticker: Stickers cannot be edited.");

            Content = newContent;
            IsEdited = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
