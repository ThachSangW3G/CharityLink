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
    }
}
