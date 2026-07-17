namespace FantasyFootball.Application.UseCases.Chat.Commands.EditMessage
{
    public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, Result<Guid>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchNotificationService _notificationService;

        public EditMessageCommandHandler(
            IChatMessageRepository chatMessageRepository,
            IUnitOfWork unitOfWork,
            IMatchNotificationService notificationService)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Result<Guid>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _chatMessageRepository.GetByIdAsync(request.MessageId);
                if (message == null)
                    return Result<Guid>.Failure(new Error("Message.NotFound", "Message not found."));

                if (message.SenderId != request.SenderId)
                    return Result<Guid>.Failure(new Error("Message.Unauthorized", "You can only edit your own messages."));

                message.Edit(request.NewContent);
                
                _chatMessageRepository.Update(message);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Broadcast via SignalR
                await _notificationService.SendMessageEditedAsync(message.RoomId, message.Id, message.Content);

                return Result<Guid>.Success(message.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
