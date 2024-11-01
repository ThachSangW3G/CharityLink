namespace CharityLink.Dtos.Comments
{
    public class UpdateCommentRequestDto
    {
        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
