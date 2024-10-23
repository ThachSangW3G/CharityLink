using CharityLink.Dtos.Communities;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class CommunityMapper
    {
        public static CommunityDto ToCommunityDto(this Community community)
        {
            return new CommunityDto
            {
                CommunityId = community.CommunityId,
                CommunityName = community.CommunityName,
                Description = community.Description,
                IsPublished = community.IsPublished,
                AdminId = community.AdminId,
                CreateDate = community.CreateDate,
                Posts = community.Posts.Select(p => p.ToPostDto()).ToList(),
                Donations = community.Donations.Select(d => d.ToDonationDto()).ToList(),
                UserCommunities = community.UserCommunities.Select(u => u.ToUserCommunityDto()).ToList(),
            };
        }
    }
}
