namespace FantasyFootball.Application.UseCases.Chat.Commands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result<Guid>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchNotificationService _notificationService;

        public SendMessageCommandHandler(
            IChatMessageRepository chatMessageRepository,
            IManagerRepository managerRepository,
            IUnitOfWork unitOfWork,
            IMatchNotificationService notificationService)
        {
            _chatMessageRepository = chatMessageRepository;
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Result<Guid>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var manager = await _managerRepository.GetByIdAsync(request.SenderId);
                if (manager == null)
                    return Result<Guid>.Failure(new Error("Manager.NotFound", "Sender not found."));

                var chatMessage = ChatMessage.Create(request.SenderId, request.RoomId, request.Content, request.Type, manager.IsPremium);

                await _chatMessageRepository.AddAsync(chatMessage);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                string broadcastMessage = $"[{manager.TeamName}] {request.Content}";
                if (request.Type == MessageType.Sticker)
                    broadcastMessage = $"[{manager.TeamName}] sent a Sticker: {request.Content}";

                await _notificationService.SendMatchUpdateAsync($"[Room: {request.RoomId}] {broadcastMessage}");

                return Result<Guid>.Success(chatMessage.Id);
            }
            catch (DomainException ex)
            {
                return Result<Guid>.Failure(new Error("Domain.Error", ex.Message));
            }
        }
    }
}
