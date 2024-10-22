using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Notifications")]
    public class Notification
    {

        public int NotificationId { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }
     
        public User Recipient { get; set; }
    }
}
