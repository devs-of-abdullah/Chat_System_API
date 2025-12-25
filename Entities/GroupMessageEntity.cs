
namespace Entities
{
    public class GroupMessageEntity
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public MessageEntity? Message { get; set; }
        public GroupEntity? Group { get; set; }

        public int GroupId { get; set; }

    }
}
