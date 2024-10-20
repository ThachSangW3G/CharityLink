using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models
{
    [Table("Users")]
    public class User
    {
        public int userID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string avatarUrl { get; set; }

        
    }
}
