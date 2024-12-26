using CharityLink.Models;

namespace CharityLink.Dtos.Users
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }


        public string AvatarUrl { get; set; }

        public string Role { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int CountDonate { get; set; }
        public int CountPosts { get; set; }


    }
}
