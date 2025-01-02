using CharityLink.Data;
using CharityLink.Dtos.Communities;
using CharityLink.Dtos.Donations;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IDonationRepository _donationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public DonationController(ApplicationDBContext applicationDBContext, IDonationRepository donationRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _applicationDBContext = applicationDBContext;
            _donationRepository = donationRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonations()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var donations = await _donationRepository.GetAllAsync();

            var donationDto = donations.Select(c => c.ToDonationDto());

            return Ok(donationDto);
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Donation>> GetCommunity([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var donation = await _donationRepository.GetByIdAsync(Id);
            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation.ToDonationDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDonationRequestDto donationRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var donation = donationRequestDto.ToDonationFromCreateDTO();

            await _donationRepository.CreateAsync(donation);

            return Ok(donation);


        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromForm] UpdateDonationRequestDto donationRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var donation = await _donationRepository.UpdateAsync(Id, donationRequestDto.ToDonationFromUpdateDTO());

            if (donation == null)
            {
                return NotFound("Donation not found");

            }

            return Ok(donation.ToDonationDto());
        }


        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var donation = await _donationRepository.DeleteAsync(Id);

            if (donation == null)
            {
                return NotFound("Donation does not exist");
            }

            return Ok(donation);
        }


        [HttpGet("get-donation-count/{CommunityId:int}")]
        public async Task<ActionResult<int>> GetDonationCount([FromRoute] int CommunityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int count = await _donationRepository.GetDonationCount(CommunityId);

            return Ok(count);
        }

        [HttpGet("get-contributors/{CommunityId:int}")]
        public async Task<ActionResult<Donation>> GetContributors([FromRoute] int CommunityId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contributors = await _donationRepository.GetContributor(CommunityId);
            var contributorDto = contributors.Select(c => c.ToDonationDto());

            var updatedContributors = new List<DonationDto>();
            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            foreach (var contributor in contributorDto)
            {
                var user = await _userRepository.GetByIdAsync(contributor.UserId);

                if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    contributor.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";
                }
                if(user != null)
                {
                    contributor.UserName = user.Name;
                }

                updatedContributors.Add(contributor);
            }

            return Ok(updatedContributors);
        }
    }
}
