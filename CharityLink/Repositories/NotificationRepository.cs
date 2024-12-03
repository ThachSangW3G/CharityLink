using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public readonly ApplicationDBContext _dBContext;

        public NotificationRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }
        public async Task CreateNotificationAsync(Notification notification)
        {
            await _dBContext.Notifications.AddAsync(notification);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId)
        {
            return await _dBContext.Notifications
           .Where(n => n.UserId == userId)
           .OrderByDescending(n => n.CreatedAt)
           .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _dBContext.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _dBContext.SaveChangesAsync();
            }
        }
    }
}
