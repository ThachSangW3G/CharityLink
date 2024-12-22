using CharityLink.Models;

namespace CharityLink.Dtos.Notifications
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
