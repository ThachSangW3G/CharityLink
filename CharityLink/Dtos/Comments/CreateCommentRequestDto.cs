﻿namespace CharityLink.Dtos.Comments
{
    public class CreateCommentRequestDto
    {

        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
