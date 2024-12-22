using CharityLink.Dtos.Comments;
using CharityLink.Dtos.Communities;
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
                CreateDate = comment.CreateDate,
                ParentCommentId = comment.ParentCommentId,
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto commentRequestDto)
        {
            return new Comment
            {
                UserId = commentRequestDto.UserId,
                PostId = commentRequestDto.PostId,
                Content = commentRequestDto.Content,
                CreateDate = commentRequestDto.CreateDate,
                ParentCommentId = commentRequestDto.ParentCommentId,
            };
        }

        public static Comment ToCommentFromUpdateDTO(this UpdateCommentRequestDto commentRequestDto)
        {
            return new Comment
            {
                UserId = commentRequestDto.UserId,
                PostId = commentRequestDto.PostId,
                Content = commentRequestDto.Content,
                CreateDate = commentRequestDto.CreateDate,
                 ParentCommentId = commentRequestDto.ParentCommentId,
            };
        }
    }
}
