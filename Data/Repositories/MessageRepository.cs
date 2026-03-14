
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
        public async Task<List<MessageEntity>> GetChatAsync(int currentUser,int otherUserId)
        {
            return await _context.Messages.AsNoTracking().Include(m => m.Sender).Include(m => m.Receiver)
                .Where(m => 
                (m.SenderId == currentUser && m.ReceiverId == otherUserId) ||
                (m.SenderId == otherUserId && m.ReceiverId == currentUser)
                )
                .OrderBy(m => m.SentAt).ToListAsync();
           
        }
        public async Task<List<AIMessageEntity>> GetAIChatAsync(int currentUser)
        { 
            return await _context.AIMessages.AsNoTracking().Where(r => r.ReceiverId == currentUser).ToListAsync();
        }
        public async Task CreateAIChatAsync(AIMessageEntity message)
        {
            await _context.AIMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
