namespace CharityLink.Dtos.Posts
{
    public class UpdatePostRequestDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public int CommunityID { get; set; }

        public DateTime createDate { get; set; }
    }
}
