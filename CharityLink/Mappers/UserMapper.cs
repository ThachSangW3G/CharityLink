using CharityLink.Dtos.Comments;
using CharityLink.Dtos.Users;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Password = user.Password,
                Role = user.Role,
                DayOfBirth = user.DayOfBirth,
            };
        }
        public static User ToUserFromCreateDTO(this CreateUserRequestDto userRequestDto)
        {
            return new User
            {
                Name = userRequestDto.Name,
                Email = userRequestDto.Email,
                PhoneNumber = userRequestDto.PhoneNumber,
                DayOfBirth = userRequestDto.DayOfBirth,
                Password = userRequestDto.Password,
            };
        }

        public static User ToUserFromUpdateDTO(this UpdateUserRequestDto userRequestDto)
        {
            return new User
            {
                Name = userRequestDto.Name,
                Email = userRequestDto.Email,
                PhoneNumber = userRequestDto.PhoneNumber,
           
                Password = userRequestDto.Password,
            };
        }
    }
}
