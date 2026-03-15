
    namespace Entities
    {
        public class AIMessageEntity
        {
            public int Id { get; set; }
            public int ReceiverId { get; set; }
            public UserEntity Receiver { get; set; } = null!;
            public string? Input { get; set; } = null!;
            public string? Output { get; set; } = null!;

            public DateTime SentAt { get; set; } = DateTime.UtcNow.AddHours(3);
        }
    }

