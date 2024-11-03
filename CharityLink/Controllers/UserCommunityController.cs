using CharityLink.Data;
using CharityLink.Dtos.Donations;
using CharityLink.Dtos.Posts;
using CharityLink.Dtos.UserCommunitys;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommunityController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IUserCommunityRepository _userCommunityRepository;

        public UserCommunityController(ApplicationDBContext applicationDBContext, IUserCommunityRepository userCommunityRepository)
        {
            _applicationDBContext = applicationDBContext;
            _userCommunityRepository = userCommunityRepository;
        }


        [HttpGet("get-community-by-user-id/{userId:int}")]
        public async Task<ActionResult<IEnumerable<Community>>> GetComunityByUserId(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var commnunities = await _userCommunityRepository.GetCommunityByUserId(userId);

            var comunityDto = commnunities.Select(c => c.ToCommunityDto());

            return Ok(comunityDto);
        }


        [HttpGet("get-user-by-community-id/{communityId:int}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByCommunityId(int communityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var users = await _userCommunityRepository.GetUserByCommunityId(communityId);

            var userDto = users.Select(c => c.ToUserDto());

            return Ok(userDto);
        }



        [HttpGet("{userId:int}/{communityId:int}/exists")]
        public async Task<ActionResult<bool>> CheckExists(int userId, int communityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _userCommunityRepository.CheckExist(userId, communityId);

            if (!exists)
            {
                return BadRequest(ModelState); 
            }

            return Ok(exists);

        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUserCommunityRequestDto userCommunityRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCommunity = userCommunityRequestDto.ToUserCommunityFromCreateDTO();

            await _userCommunityRepository.CreateAsync(userCommunity);

            return Ok("Create successfully");


        }


        [HttpDelete]
        [Route("{userId:int}/{communityId:int}")]
        public async Task<ActionResult> Delete([FromRoute] int userId, int communityId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userCommunity = await _userCommunityRepository.DeleteAsync(userId, communityId);

            if (userCommunity == null)
            {
                return NotFound("UserCommunity does not exist");
            }

            return Ok(userCommunity);
        }

    }
}
