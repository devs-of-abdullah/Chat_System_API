

namespace Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public string Content { get; set; } = string.Empty;
        public PrivateMessageEntity? PrivateMessage { get; set; }
        public GroupMessageEntity? GroupMessage { get; set; }
    }
}
