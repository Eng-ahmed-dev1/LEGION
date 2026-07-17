namespace FantasyFootball.Application.UseCases.Chat.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Result<bool>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchNotificationService _notificationService;

        public DeleteMessageCommandHandler(
            IChatMessageRepository chatMessageRepository,
            IUnitOfWork unitOfWork,
            IMatchNotificationService notificationService)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Result<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _chatMessageRepository.GetByIdAsync(request.MessageId);
                if (message == null)
                    return Result<bool>.Failure(new Error("Message.NotFound", "Message not found."));

                // Only sender can delete their message
                if (message.SenderId != request.SenderId)
                    return Result<bool>.Failure(new Error("Message.Unauthorized", "You can only delete your own messages."));

                var roomId = message.RoomId;
                var messageId = message.Id;

                _chatMessageRepository.Delete(message);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Broadcast via SignalR
                await _notificationService.SendMessageDeletedAsync(roomId, messageId);

                return Result<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return Result<bool>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
