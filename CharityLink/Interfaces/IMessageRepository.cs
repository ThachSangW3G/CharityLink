using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
        Task CreateMessageAsync(Message message);
        Task<IEnumerable<Message>> GetLatestMessagesForUser(int userId);
        Task<int> CountUnreadMessages(int userId, int otherUserId);
        Task MarkMessagesAsRead(int userId, int otherUserId);
        Task<User?> GetUserByIdUser(int userId);
    }
}
