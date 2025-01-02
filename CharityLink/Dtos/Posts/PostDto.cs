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

        public string? ImageUrl { get; set; }

        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public String UserName { get; set; }
        public String AvatarUrl { get; set; }

        public String CommunityName { get; set; }
        public int Type { get; set; }
        public bool IsLiked { get; set; }
       
    }
}
