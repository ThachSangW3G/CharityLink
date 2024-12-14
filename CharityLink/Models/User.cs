using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Users")]
    public class User { 
  
        public int UserId { get; set; }

   
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

 
        public string? AvatarUrl { get; set; }

 
        public List<Like> Likes { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Donation> Donations { get; set; }
        public List<Notification> Notifications { get; set; }

        public List<UserCommunity> UserCommunities { get; set; }
    }
}
