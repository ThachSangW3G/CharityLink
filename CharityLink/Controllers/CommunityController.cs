using CharityLink.Data;
using CharityLink.Dtos.Communities;
using CharityLink.Dtos.Users;
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

            var communityDto = communities.Select(community =>
            {
                var dto = community.ToCommunityDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{Request.Scheme}://{Request.Host}{dto.ImageUrl}";
                }
                return dto;
            });

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

            var communityDto = community.ToCommunityDto();

            if (!string.IsNullOrEmpty(communityDto.ImageUrl))
            {
                communityDto.ImageUrl = $"{Request.Scheme}://{Request.Host}{communityDto.ImageUrl}";
            }
            return Ok(communityDto);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateCommunityRequestDto communityDto, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string imageUrl = string.Empty;
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "image_communitys");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageUrl = $"/image_communitys/{uniqueFileName}";
            }

            var community = communityDto.ToCommunityFromCreateDTO();

            community.ImageUrl = imageUrl;

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

        [HttpGet("{communityId:int}/get-posts")]
        public async Task<ActionResult<List<Post>>> GetPostsByCommunityId(int communityId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _communityRepository.GetPostByCommunityId(communityId);

            var postDto = posts.Select(p => p.ToPostDto()).ToList();

            return Ok(postDto);

        }


        [HttpGet("{communityId:int}/get-donations")]
        public async Task<ActionResult<List<Donation>>> GetDoantionsByCommunityId(int communityId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var donations = await _communityRepository.GetDonationByCommunityId(communityId);

            var donationDto = donations.Select(p => p.ToDonationDto()).ToList();

            return Ok(donationDto);

        }

        [HttpGet("{communityId:int}/get-amount-for-community")]
        public async Task<ActionResult<decimal>> GetAmountForCommunity(int communityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var amount = await _communityRepository.GetAmountDonationForCommunity(communityId);

            return Ok(amount);
        }


        [HttpGet("get-upcoming")]
        public async Task<ActionResult<IEnumerable<Community>>> GetUpcoming()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var communities = await _communityRepository.GetUpComing();

            var communiyDto = communities.Select(c => c.ToCommunityDto()).ToList();
            return Ok(communiyDto);
        }

        [HttpGet("get-ongoing")]
        public async Task<ActionResult<IEnumerable<Community>>> GetOnGoing()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var communities = await _communityRepository.GetOnGoing();

            var communiyDto = communities.Select(c => c.ToCommunityDto()).ToList();
            return Ok(communiyDto);
        }

        [HttpGet("get-completed")]
        public async Task<ActionResult<IEnumerable<Community>>> GetCompleted()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var communities = await _communityRepository.GetCompleted();

            var communiyDto = communities.Select(c => c.ToCommunityDto()).ToList();
            return Ok(communiyDto);
        }


    }
}
 