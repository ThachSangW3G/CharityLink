using CharityLink.Data;
using CharityLink.Dtos.Communities;
using CharityLink.Dtos.Users;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ICommunityRepository _communityRepository;
        private readonly IConfiguration _configuration;


        public CommunityController(ApplicationDBContext applicationDBContext, ICommunityRepository communityRepository, IConfiguration configuration)
        {
            _applicationDBContext = applicationDBContext;
            _communityRepository = communityRepository;
            _configuration = configuration;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetCommunites()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var communities = await _communityRepository.GetAllAsync();

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";


            var communityDto = communities.Select(community =>
            {
                var dto = community.ToCommunityDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{baseUrl}{dto.ImageUrl}";
                }
                return dto;
            });



            // Tạo danh sách các tác vụ bất đồng bộ để lấy dữ liệu cần thiết
            var tasks = communityDto.Select(async dto =>
            {
                dto.CurrentAmount = await _communityRepository.GetAmountDonationForCommunity(dto.CommunityId);
                dto.DonationCount = await _communityRepository.GetDonationCount(dto.CommunityId);
                return dto;
            }).ToList();

            // Chờ tất cả các tác vụ hoàn thành
            var updatedCommunityDtoList = await Task.WhenAll(tasks);

            return Ok(updatedCommunityDtoList);
        }

        [HttpGet("/api/Community/Nopublic")]
        public async Task<ActionResult<IEnumerable<Community>>> GetCommunitiesNoPublic()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var communities = await _communityRepository.GetAllNoPublic();

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";


            var communityDto = communities.Select(community =>
            {
                var dto = community.ToCommunityDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{baseUrl}{dto.ImageUrl}";
                }
                return dto;
            });



            // Tạo danh sách các tác vụ bất đồng bộ để lấy dữ liệu cần thiết
            var tasks = communityDto.Select(async dto =>
            {
                dto.CurrentAmount = await _communityRepository.GetAmountDonationForCommunity(dto.CommunityId);
                dto.DonationCount = await _communityRepository.GetDonationCount(dto.CommunityId);
                return dto;
            }).ToList();

            // Chờ tất cả các tác vụ hoàn thành
            var updatedCommunityDtoList = await Task.WhenAll(tasks);

            return Ok(updatedCommunityDtoList);
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
            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            var communityDto = community.ToCommunityDto();

            if (!string.IsNullOrEmpty(communityDto.ImageUrl))
            {
                communityDto.ImageUrl = $"{baseUrl}{communityDto.ImageUrl}";
            }

            communityDto.CurrentAmount = await _communityRepository.GetAmountDonationForCommunity(communityDto.CommunityId);

            communityDto.DonationCount = await _communityRepository.GetDonationCount(communityDto.CommunityId);
            

            return Ok(communityDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateCommunityRequestDto communityDto, IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();

                    return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
                }

                string imageUrl = string.Empty;

                if (image != null && image.Length > 0)
                {
                    try
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
                    catch (Exception ex)
                    {
                        return StatusCode(500, new { message = "Lỗi khi xử lý file ảnh", details = ex.Message });
                    }
                }

                var community = communityDto.ToCommunityFromCreateDTO();
                community.ImageUrl = imageUrl;

                await _communityRepository.CreateAsync(community);

                return Ok(new { message = "Dự án đã được tạo thành công", data = community });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");

                return StatusCode(500, new { message = "Có lỗi xảy ra trên server", details = ex.Message });
            }
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

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";


            var communityDto = communities.Select(community =>
            {
                var dto = community.ToCommunityDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{baseUrl}{dto.ImageUrl}";
                }
                return dto;
            });



            // Tạo danh sách các tác vụ bất đồng bộ để lấy dữ liệu cần thiết
            var tasks = communityDto.Select(async dto =>
            {
                dto.CurrentAmount = await _communityRepository.GetAmountDonationForCommunity(dto.CommunityId);
                dto.DonationCount = await _communityRepository.GetDonationCount(dto.CommunityId);
                return dto;
            }).ToList();

            // Chờ tất cả các tác vụ hoàn thành
            var updatedCommunityDtoList = await Task.WhenAll(tasks);

            return Ok(updatedCommunityDtoList);
        }

        [HttpGet("get-ongoing")]
        public async Task<ActionResult<IEnumerable<Community>>> GetOnGoing()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var communities = await _communityRepository.GetOnGoing();

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";


            var communityDto = communities.Select(community =>
            {
                var dto = community.ToCommunityDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{baseUrl}{dto.ImageUrl}";
                }
                return dto;
            });



            // Tạo danh sách các tác vụ bất đồng bộ để lấy dữ liệu cần thiết
            var tasks = communityDto.Select(async dto =>
            {
                dto.CurrentAmount = await _communityRepository.GetAmountDonationForCommunity(dto.CommunityId);
                dto.DonationCount = await _communityRepository.GetDonationCount(dto.CommunityId);
                return dto;
            }).ToList();

            // Chờ tất cả các tác vụ hoàn thành
            var updatedCommunityDtoList = await Task.WhenAll(tasks);

            return Ok(updatedCommunityDtoList);
        }

        [HttpGet("get-completed")]
        public async Task<ActionResult<IEnumerable<Community>>> GetCompleted()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var communities = await _communityRepository.GetCompleted();

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";


            var communityDto = communities.Select(community =>
            {
                var dto = community.ToCommunityDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{baseUrl}{dto.ImageUrl}";
                }
                return dto;
            });



            // Tạo danh sách các tác vụ bất đồng bộ để lấy dữ liệu cần thiết
            var tasks = communityDto.Select(async dto =>
            {
                dto.CurrentAmount = await _communityRepository.GetAmountDonationForCommunity(dto.CommunityId);
                dto.DonationCount = await _communityRepository.GetDonationCount(dto.CommunityId);
                return dto;
            }).ToList();

            // Chờ tất cả các tác vụ hoàn thành
            var updatedCommunityDtoList = await Task.WhenAll(tasks);

            return Ok(updatedCommunityDtoList);
        }


    }
}
 