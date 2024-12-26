    using CharityLink.Data;
using CharityLink.Dtos.Posts;
using CharityLink.Dtos.Users;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPostRepository _postRepository;
        private readonly IDonationRepository _donationRepository;

        public UserController(ApplicationDBContext applicationDBContext, IUserRepository userRepository, IConfiguration configuration, IPostRepository postRepository, IDonationRepository donationRepository)
        {
            _applicationDBContext = applicationDBContext;
            _userRepository = userRepository;
            _configuration = configuration;
            _postRepository = postRepository;
            _donationRepository = donationRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var users = await _userRepository.GetAllAsync();

            var updatedUserDtos = new List<UserDto>();
            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";


            var userDtos = users.Select(user =>
            {
                var dto = user.ToUserDto();
                if (!string.IsNullOrEmpty(dto.AvatarUrl))
                {

                    dto.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";
                }
                return dto;
            });

            foreach (var userDto in userDtos)
            {
                userDto.CountPosts = await _postRepository.GetCountPostByUser(userDto.UserId);
                userDto.CountDonate = await _donationRepository.GetCountDonationByUser(userDto.UserId);

                updatedUserDtos.Add(userDto);
            }

            return Ok(updatedUserDtos);
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.GetByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = user.ToUserDto();

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            if (!string.IsNullOrEmpty(userDto.AvatarUrl))
            {
                userDto.AvatarUrl = $"{baseUrl}{userDto.AvatarUrl}";
            }

            userDto.CountPosts = await _postRepository.GetCountPostByUser(userDto.UserId);
            userDto.CountDonate = await _donationRepository.GetCountDonationByUser(userDto.UserId);

            return Ok( userDto );
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateUserRequestDto userRequestDto, IFormFile avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string avatarUrl = string.Empty;
            if (avatar != null && avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "avatars");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(avatar.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }

                avatarUrl = $"/avatars/{uniqueFileName}";
            }

            var user = userRequestDto.ToUserFromCreateDTO();

            user.AvatarUrl = avatarUrl;

            await _userRepository.CreateAsync(user);

            return Ok();


        }



        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromForm] UpdateUserRequestDto userRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.UpdateAsync(Id, userRequestDto.ToUserFromUpdateDTO());

            if (user == null)
            {
                return NotFound("User not found");

            }

            return Ok(user.ToUserDto());
        }



        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.DeleteAsync(Id);

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(user);
        }

        [HttpGet("get-by-email")]
        public async Task<ActionResult<User>> GetByEmail([FromQuery] string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = user.ToUserDto();

            if (!string.IsNullOrEmpty(userDto.AvatarUrl))
            {
                var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
                userDto.AvatarUrl = $"{baseUrl}{userDto.AvatarUrl}";
               
            }

            userDto.CountPosts = await _postRepository.GetCountPostByUser(userDto.UserId);
            userDto.CountDonate = await _donationRepository.GetCountDonationByUser(userDto.UserId);

            return Ok(userDto);
        }


        [HttpPut]
        [Route("change-avatar/{userId:int}")]
        public async Task<ActionResult<User?>> ChangeAvatar([FromRoute] int userId, IFormFile avatar)
        {
            string avatarUrl = string.Empty;
            if (avatar != null && avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "avatars");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(avatar.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }

                avatarUrl = $"/avatars/{uniqueFileName}";
            }

            var user = await _userRepository.ChangeAvatar(userId, avatarUrl);

            



            if (user == null)
            {
                return NotFound();
            }else
            {
                var userDto = user.ToUserDto();
                var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
                userDto.AvatarUrl = $"{baseUrl}{userDto.AvatarUrl}";

                userDto.CountPosts = await _postRepository.GetCountPostByUser(userDto.UserId);
                userDto.CountDonate = await _donationRepository.GetCountDonationByUser(userDto.UserId);

                return Ok(userDto);
            }
        }

    }
}
