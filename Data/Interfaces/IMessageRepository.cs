
using Entities;

namespace Data
{
    public interface IMessageRepository
    {
        Task CreateMessageAsync(MessageEntity message);
        Task<List<MessageEntity>> GetChatAsync(int currentUserId, int otherUserId);
        Task CreateAIChatAsync(AIMessageEntity message);
        Task<List<AIMessageEntity>> GetAIChatAsync(int currentUserId);
    }
}
