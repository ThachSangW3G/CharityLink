using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        private readonly ApplicationDBContext _dBContext;

        public MessageRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<int> CountUnreadMessages(int userId, int otherUserId)
        {
            return await _dBContext.Messages.Where(m => m.SenderId == otherUserId && m.ReceiverId == userId && !m.IsRead).CountAsync();
        }

        public async Task CreateMessageAsync(Message message)
        {
            await _dBContext.Messages.AddAsync(message);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetLatestMessagesForUser(int userId)
        {
            return await _dBContext.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId :  m.SenderId)
                .Select(group => group.OrderByDescending(m => m.SentAt).First()).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2)
        {
            return await _dBContext.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdUser(int userId)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task MarkMessagesAsRead(int userId, int otherUserId)
        {
            await _dBContext.Messages
                .Where(m => m.SenderId == otherUserId && m.ReceiverId == userId && !m.IsRead)
                .ForEachAsync(m => m.IsRead = true);

            await _dBContext.SaveChangesAsync();
        }
    }
}
