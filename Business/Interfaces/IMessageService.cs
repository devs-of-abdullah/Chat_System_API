
using DTOs;
namespace Business
{
    public interface IMessageService
    {
        Task SendAsync(int senderId, SendMessageDto dto);
        Task<List<MessageDto>> GetConversationAsync(int userId,int otherUserId);

      

    }
}
