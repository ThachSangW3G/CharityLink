using CharityLink.Data;
using CharityLink.Dtos.Posts;
using CharityLink.Dtos.Users;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IUserRepository _userRepository;

        public UserController(ApplicationDBContext applicationDBContext, IUserRepository userRepository)
        {
            _applicationDBContext = applicationDBContext;
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var users = await _userRepository.GetAllAsync();

            var userDto = users.Select(c => c.ToUserDto());

            return Ok(userDto);
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

            return Ok(user.ToUserDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUserRequestDto userRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userRequestDto.ToUserFromCreateDTO();

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

    }
}
