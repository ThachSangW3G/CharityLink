using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Comments")]
    public class Comment
    {
 
        public int CommentId { get; set; }

        public string Content { get; set; }


        public int UserId { get; set; }

        public User Author { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
