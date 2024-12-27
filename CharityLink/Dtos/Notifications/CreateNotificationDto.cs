using CharityLink.Models;

namespace CharityLink.Dtos.Notifications
{
    public class CreateNotificationDto
    {
        public string? Content { get; set; }

        public string? Title { get; set; }

        public int UserId { get; set; }

        public int SenderId { get; set; }


        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string Type { get; set; }
        public int ComunityId { get; set; }
        public int PostId { get; set; }
    }
}
