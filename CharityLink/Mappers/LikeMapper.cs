using CharityLink.Dtos.Comments;
using CharityLink.Dtos.Likes;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class LikeMapper
    {

        public static LikeDto ToLikeDto(this Like like)
        {
            return new LikeDto
            {
                LikeId = like.LikeId,
                UserId = like.UserId,
                PostId = like.PostId,
                LikeAt = like.LikeAt,
            };
        }


        public static Like ToLikeFromCreateDTO(this CreateLikeRequestDto likeRequestDto)
        {
            return new Like
            {
                UserId = likeRequestDto.UserId,
                PostId = likeRequestDto.PostId,
                LikeAt = likeRequestDto.LikeAt,
            };
        }

        public static Like ToLikeFromUpdateDTO(this UpdateLikeRequestDto likeRequestDto)
        {
            return new Like
            {
                UserId = likeRequestDto.UserId,
                PostId = likeRequestDto.PostId,
                LikeAt = likeRequestDto.LikeAt,
            };
        }
    }
}
