using CharityLink.Data;
using CharityLink.Dtos.Communities;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ICommunityRepository _communityRepository;


        public CommunityController(ApplicationDBContext applicationDBContext, ICommunityRepository communityRepository)
        {
            _applicationDBContext = applicationDBContext;
            _communityRepository = communityRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetCommunites()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var communities = await _communityRepository.GetAllAsync();

            var communityDto = communities.Select(c => c.ToCommunityDto());

            return Ok(communityDto);
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Community>> GetCommunity([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var community = await _communityRepository.GetByIdAsync(Id);
            if (community == null)
            {
                return NotFound();
            }

            return Ok(community.ToCommunityDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCommunityRequestDto communityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var community = communityDto.ToCommunityFromCreateDTO();

            await _communityRepository.CreateAsync(community);

            return Ok();


        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromForm] UpdateCommunityRequestDto communityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var community = await _communityRepository.UpdateAsync(Id, communityDto.ToCommunityFromUpdateDTO());

            if (community == null)
            {
                return NotFound("Community not found");

            }

            return Ok(community.ToCommunityDto());
        }


        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var community = await _communityRepository.DeleteAsync(Id);

            if (community == null)
            {
                return NotFound("Community does not exist");
            }

            return Ok(community);
        }
    }
}
 