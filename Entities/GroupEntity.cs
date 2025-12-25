

namespace Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GroupMessageEntity>? Messages { get; set; }
        public List<UserEntity>? Users { get; set; } 

    }
}
