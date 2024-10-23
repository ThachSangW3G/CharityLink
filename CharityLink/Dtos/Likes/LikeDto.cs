using CharityLink.Models;

namespace CharityLink.Dtos.Likes
{
    public class LikeDto
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }

        public int PostId { get; set; }
        public DateTime LikeAt { get; set; }
    }
}
