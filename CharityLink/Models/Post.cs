using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Posts")]
    public class Post
    {

        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }


        public int UserId { get; set; }

        public User Author { get; set; }

        public int CommunityID { get; set; }

        public Community Community { get; set; }

        public DateTime createDate { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Like> Likes { get; set; }  


    }
}
