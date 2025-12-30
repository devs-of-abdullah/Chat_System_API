
using Business.Interfaces;
using Data;
using Entities;
using Entities.DTOs;

namespace Business
{
    public class MessageService : IMessageService
    {
        readonly IMessageRepository _repo;
        readonly IMessageNotifier _notifier;
        public MessageService(IMessageRepository repo, IMessageNotifier notifier)
        { 
            _notifier = notifier;
            _repo = repo;
        }
        public async Task SendAsync(int senderId, SendMessageDto dto)
        {
            var entity = new MessageEntity
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Message = dto.Message,
            };

            await _repo.AddAsync(entity);

            await _notifier.NotifyAsync(dto.ReceiverId, new MessageDto
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Message = dto.Message,
                SentAt = entity.SentAt,
            });
        }
        public async Task<List<MessageDto>> GetConversationAsync(int userId, int otherUserId)
        { 
            var data = await _repo.GetConversationAsync(userId, otherUserId);
            return data.Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                SenderUsername  = m.Sender.Username,
                ReceiverUsername = m.Receiver.Username,
                Message = m.Message,
                SentAt = m.SentAt,

            }).ToList();
        } 
    }
}
