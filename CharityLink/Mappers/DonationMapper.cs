using CharityLink.Dtos.Donations;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class DonationMapper
    {
        public static DonationDto ToDonationDto (this Donation donation)
        {
            return new DonationDto
            {
                DonationId = donation.DonationId,
                Amount = donation.Amount,
                UserId = donation.UserId,
                CommunityId = donation.CommunityId,
                donateDate = donation.donateDate,
            };
        }
    }
}
