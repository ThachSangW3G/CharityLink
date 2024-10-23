using CharityLink.Dtos.Comments;
using CharityLink.Dtos.Likes;
using CharityLink.Models;

namespace CharityLink.Dtos.Posts
{
    public class PostDto
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }


        public int UserId { get; set; }


        public int CommunityID { get; set; }


        public DateTime createDate { get; set; }

        public List<CommentDto> Comments { get; set; }

        public List<LikeDto> Likes { get; set; }
    }
}
