namespace CharityLink.Dtos.Authentications
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DayOfBirth { get; set; }
    }
}
