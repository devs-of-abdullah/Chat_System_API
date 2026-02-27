using Business.Interfaces;
using Data;
using DTOs;
using Entities;
using Microsoft.Extensions.Logging; 


namespace Business
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly IMessageNotifier _notifier;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IMessageRepository repo, IMessageNotifier notifier, ILogger<MessageService> logger)
        {
            _repo = repo;
            _notifier = notifier;
            _logger = logger;
        }

        public async Task SendMessageAsync(int senderId, SendMessageDto dto)
        {
            try
            {
                var entity = new MessageEntity
                {
                    SenderId = senderId,
                    ReceiverId = dto.ReceiverId,
                    Message = dto.Message
                };

                await _repo.CreateMessageAsync(entity);

                var messageDto = new MessageDto
                {
                    Id = entity.Id,
                    SenderId = senderId,
                    ReceiverId = dto.ReceiverId,
                    Message = dto.Message,
                    SentAt = entity.SentAt
                };

                await _notifier.NotifyAsync(dto.ReceiverId, messageDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message from {SenderId} to {ReceiverId}", senderId, dto.ReceiverId);
                throw;
            }
        }

        public async Task<List<MessageDto>> GetConversationAsync(int userId, int otherUserId)
        {
            try
            {
                var data = await _repo.GetConversationAsync(userId, otherUserId);
                return data.Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    SenderUsername = m.Sender.Username,
                    ReceiverUsername = m.Receiver.Username,
                    Message = m.Message,
                    SentAt = m.SentAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve conversation between {UserId} and {OtherUserId}", userId, otherUserId);
                throw;
            }
        }
    }
}