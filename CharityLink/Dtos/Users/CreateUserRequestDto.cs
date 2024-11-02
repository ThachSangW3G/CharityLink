namespace CharityLink.Dtos.Users
{
    public class CreateUserRequestDto
    {
       
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }


        public string AvatarUrl { get; set; }
    }
}
