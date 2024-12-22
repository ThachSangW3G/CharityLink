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
                StartDate = community.StartDate,
                EndDate = community.EndDate,
                TargetAmount = community.TargetAmount,
                ImageUrl = community.ImageUrl,
                Type = community.Type,
                Longitude = community.Longitude,
                Latitude = community.Latitude,
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
                StartDate= communityRequestDto.StartDate,
                EndDate= communityRequestDto.EndDate,
                TargetAmount= communityRequestDto.TargetAmount,
                Type = communityRequestDto.Type,
                Latitude = communityRequestDto.Latitude,
                Longitude = communityRequestDto.Longitude,
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
                StartDate= communityRequestDto.StartDate,
                EndDate= communityRequestDto.EndDate,
                TargetAmount = communityRequestDto.TargetAmount
            };
        }
    }
}
