using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId);
        Task CreateNotificationAsync(Notification notification);
        Task MarkAsReadAsync(int notificationId);
    }
}
