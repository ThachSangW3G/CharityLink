using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("UserCommunities")]
    public class UserCommunity
    {

        public int UserId { get; set; }
        public User User { get; set; }

  
        public int CommunityId { get; set; }

        public Community Community { get; set; }
    }
}
