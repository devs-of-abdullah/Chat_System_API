
using System.ComponentModel.DataAnnotations;

namespace DTOs
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
        [Required]
        public int ReceiverId { get; set; }

        [Required,MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; } = string.Empty;
    }


}
