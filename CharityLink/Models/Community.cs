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

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 

        public decimal? TargetAmount { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }


        public List<Post> Posts { get; set; }
        public List<Donation> Donations { get; set; }

        public List<UserCommunity> UserCommunities { get; set; }
    }
}
