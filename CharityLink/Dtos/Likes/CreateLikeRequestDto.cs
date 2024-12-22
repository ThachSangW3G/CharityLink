namespace CharityLink.Dtos.Likes
{
    public class CreateLikeRequestDto
    {
        public int UserId { get; set; }

        public int PostId { get; set; }
        public DateTime LikeAt { get; set; }
    }
}
