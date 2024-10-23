using CharityLink.Models;

namespace CharityLink.Dtos.Donations
{
    public class DonationDto
    {
        public int DonationId { get; set; }


        public decimal Amount { get; set; }


        public int UserId { get; set; }

        public int CommunityId { get; set; }

        public DateTime donateDate { get; set; }
    }
}
