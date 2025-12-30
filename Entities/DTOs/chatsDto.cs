
namespace Entities.DTOs
{

    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername {  get; set; } = string.Empty;
        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } 
    }
    public class SendMessageDto
    {
        public int ReceiverId { get; set; }
        public string Message { get; set; } = string.Empty;

    }

  
}
