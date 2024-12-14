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
        public async Task CreateMessageAsync(Message message)
        {
            await _dBContext.Messages.AddAsync(message);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2)
        {
            return await _dBContext.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
