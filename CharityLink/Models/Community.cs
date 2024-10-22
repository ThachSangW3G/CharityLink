using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Communities")]
    public class Community
    {
       
        public int CommunityId {  get; set; }

        public string CommunityName { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public int AdminId { get; set; }

        public User Admin { get; set; }

        public DateTime CreateDate { get; set; }


        public List<Post> Posts { get; set; }
        public List<Donation> Donations { get; set; }

        public List<UserCommunity> UserCommunities { get; set; }
    }
}
