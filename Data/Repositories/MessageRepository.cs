
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MessageRepository : IMessageRepository
    {
        readonly AppDbContext _context;
        public MessageRepository(AppDbContext context) {_context = context;}

        public async Task CreateMessageAsync(MessageEntity message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MessageEntity>> GetConversationAsync(int currentUser,int otherUserId)
        {
            return await _context.Messages.AsNoTracking().Include(m => m.Sender).Include(m => m.Receiver)
                .Where(m => 
                (m.SenderId == currentUser && m.ReceiverId == otherUserId) ||
                (m.SenderId == otherUserId && m.ReceiverId == currentUser)
                )
                .OrderBy(m => m.SentAt).ToListAsync();
           
        }
    }
}
