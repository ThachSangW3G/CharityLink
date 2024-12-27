using CharityLink.Dtos.Notifications;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationDto ToNotificationDto (this Notification notification)
        {
            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                UserId = notification.UserId,
                Title = notification.Title,
                Content = notification.Content,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
                ComunityId = notification.ComunityId,
                PostId = notification.PostId,
                SenderId = notification.SenderId,
                Type = notification.Type,
            };
        }

        public static Notification ToNotificationFromCreateDto( this CreateNotificationDto notificationDto)
        {
            return new Notification
            {
                UserId = notificationDto.UserId,
                Title = notificationDto.Title,
                Content = notificationDto.Content,
                IsRead = notificationDto.IsRead,
                CreatedAt = notificationDto.CreatedAt,
                ComunityId=notificationDto.ComunityId,
                PostId=notificationDto.PostId,
                SenderId=notificationDto.SenderId,
                Type = notificationDto.Type,
            };
        }
    }
}
