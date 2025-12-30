

namespace Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }

        public int SenderId { get; set;}
        public UserEntity Sender { get; set; } = null!;

        public int ReceiverId {  get; set; }
        public UserEntity Receiver { get; set; } = null!;

        
        public string Message { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
    }
}
