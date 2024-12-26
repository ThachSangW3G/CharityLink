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
        public string AvatarUrl { get; set; }
        public string UserName { get; set; }
    }
}
