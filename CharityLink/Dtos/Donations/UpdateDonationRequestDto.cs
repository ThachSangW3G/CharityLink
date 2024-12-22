namespace CharityLink.Dtos.Donations
{
    public class UpdateDonationRequestDto
    {
    
        public decimal Amount { get; set; }


        public int UserId { get; set; }

        public int CommunityId { get; set; }

        public DateTime donateDate { get; set; }
    }
}
