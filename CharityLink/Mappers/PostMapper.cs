using CharityLink.Dtos.Posts;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto (this Post post)
        {
            return new PostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                CommunityID = post.CommunityID,
                createDate = post.createDate,
                ImageUrl = post.ImageUrl,
                Type = post.Type,
                //Comments = post.Comments.Select(c => c.ToCommentDto()).ToList(),
                //Likes = post.Likes.Select(l => l.ToLikeDto()).ToList(),
            };
        }

        public static Post ToPostFromCreateDTO(this CreatePostRequestDto postRequestDto)
        {
            return new Post
            {
                Title = postRequestDto.Title,
                Content = postRequestDto.Content,
                UserId = postRequestDto.UserId,
                CommunityID = postRequestDto.CommunityID,
                createDate = postRequestDto.createDate,
            };
        }

        public static Post ToPostFromUpdateDTO(this UpdatePostRequestDto postRequestDto)
        {
            return new Post
            {
                Title = postRequestDto.Title,
                Content = postRequestDto.Content,
                UserId = postRequestDto.UserId,
                CommunityID = postRequestDto.CommunityID,
                createDate = postRequestDto.createDate,
            };
        }
    }
}
