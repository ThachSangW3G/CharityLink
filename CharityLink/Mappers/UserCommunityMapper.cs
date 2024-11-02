using CharityLink.Dtos.UserCommunitys;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class UserCommunityMapper
    {
        public static UserCommunityDto ToUserCommunityDto(this UserCommunity userCommunity)
        {
            return new UserCommunityDto
            {
                UserId = userCommunity.UserId,
                CommunityId = userCommunity.CommunityId,
            };

        }

    }
}
