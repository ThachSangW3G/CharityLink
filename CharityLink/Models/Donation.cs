using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Donations")]
    public class Donation
    {

        public int DonationId { get; set; }

  
        public decimal Amount { get; set; }


        public int UserId { get; set; }

        public User Donor { get; set; }


        public int CommunityId { get; set; }

 
        public Community Community { get; set; }
        public DateTime donateDate { get; set; }
    }
}
