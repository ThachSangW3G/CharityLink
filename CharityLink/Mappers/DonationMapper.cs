using CharityLink.Dtos.Donations;
using CharityLink.Dtos.Likes;
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

        public static Donation ToDonationFromCreateDTO(this CreateDonationRequestDto donationRequestDto)
        {
            return new Donation
            {
                Amount = donationRequestDto.Amount,
                UserId = donationRequestDto.UserId,
                CommunityId = donationRequestDto.CommunityId,
                donateDate = donationRequestDto.donateDate,
            };
        }

        public static Donation ToDonationFromUpdateDTO(this UpdateDonationRequestDto donationRequestDto)
        {
            return new Donation
            {
                Amount = donationRequestDto.Amount,
                UserId = donationRequestDto.UserId,
                CommunityId = donationRequestDto.CommunityId,
                donateDate = donationRequestDto.donateDate,
            };
        }
    }
}
