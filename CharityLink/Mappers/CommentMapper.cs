using CharityLink.Dtos.Comments;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                CommentId = comment.CommentId,
                UserId = comment.UserId,
                PostId = comment.PostId,
                Content = comment.Content,
            };
        }
    }
}
