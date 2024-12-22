using CharityLink.Dtos.Donations;
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

        public static UserCommunity ToUserCommunityFromCreateDTO(this CreateUserCommunityRequestDto userCommunityRequestDto)
        {
            return new UserCommunity
            {
                UserId = userCommunityRequestDto.UserId,
                CommunityId = userCommunityRequestDto.CommunityId,
            };
        }

    }
}
