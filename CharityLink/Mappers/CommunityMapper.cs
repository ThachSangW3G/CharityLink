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
                //Posts = community.Posts?.Select(p => p.ToPostDto()).ToList() ?? null,
                //Donations = community.Donations?.Select(d => d.ToDonationDto()).ToList() ?? null,
                //UserCommunities = community.UserCommunities?.Select(u => u.ToUserCommunityDto()).ToList() ?? null,
            };
        }

        public static Community ToCommunityFromCreateDTO(this CreateCommunityRequestDto communityRequestDto)
        {
            return new Community
            {
                CommunityName= communityRequestDto.CommunityName,
                Description = communityRequestDto.Description,
                IsPublished= communityRequestDto.IsPublished,
                AdminId= communityRequestDto.AdminId,
                CreateDate= communityRequestDto.CreateDate,
            };
        }

        public static Community ToCommunityFromUpdateDTO(this UpdateCommunityRequestDto communityRequestDto)
        {
            return new Community
            {
                CommunityName= communityRequestDto.CommunityName,
                Description = communityRequestDto.Description,
                IsPublished= communityRequestDto.IsPublished,
                AdminId= communityRequestDto.AdminId,
                CreateDate= communityRequestDto.CreateDate,
            };
        }
    }
}
