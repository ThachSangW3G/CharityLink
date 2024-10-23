using CharityLink.Dtos.Donations;
using CharityLink.Dtos.Posts;
using CharityLink.Dtos.UserCommunity;
using CharityLink.Models;

namespace CharityLink.Dtos.Communities
{
    public class CommunityDto
    {
        public int CommunityId { get; set; }

        public string CommunityName { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public int AdminId { get; set; }

        public DateTime CreateDate { get; set; }
        public List<PostDto>? Posts { get; set; }
        public List<DonationDto>? Donations { get; set; }

        public List<UserCommunityDto>? UserCommunities { get; set; }

    }
}
