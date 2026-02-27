
using Entities;

namespace Data
{
    public interface IMessageRepository
    {
        Task CreateMessageAsync(MessageEntity message);
        Task<List<MessageEntity>> GetConversationAsync(int currentUserId, int otherUserId);
    }
}
