
using Entities;

namespace Data
{
    public interface IMessageRepository
    {
        Task AddAsync(MessageEntity message);
        Task<List<MessageEntity>> GetConversationAsync(int userId, int otherUserId);
    }
}
