using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
        Task CreateMessageAsync(Message message);
    }
}
