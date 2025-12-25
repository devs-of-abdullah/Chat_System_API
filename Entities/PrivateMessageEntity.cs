

namespace Entities
{
    public class PrivateMessageEntity
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public MessageEntity? Message { get; set; }
        public int ReceiverId { get; set; }

    }
}
