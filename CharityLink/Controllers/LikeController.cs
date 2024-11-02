using CharityLink.Data;
using CharityLink.Dtos.Comments;
using CharityLink.Dtos.Likes;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ILikeRepository _likeRepository;

        public LikeController(ApplicationDBContext applicationDBContext, ILikeRepository likeRepository)
        {
            _applicationDBContext = applicationDBContext;
            _likeRepository = likeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var likes = await _likeRepository.GetAllAsync();

            var likeDto = likes.Select(c => c.ToLikeDto());

            return Ok(likeDto);
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Post>> GetComment([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var like = await _likeRepository.GetByIdAsync(Id);
            if (like == null)
            {
                return NotFound();
            }

            return Ok(like.ToLikeDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateLikeRequestDto likeRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var like = likeRequestDto.ToLikeFromCreateDTO();

            await _likeRepository.CreateAsync(like);

            return Ok();


        }



        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromForm] UpdateLikeRequestDto likeRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var like = await _likeRepository.UpdateAsync(Id, likeRequestDto.ToLikeFromUpdateDTO());

            if (like == null)
            {
                return NotFound("Like not found");

            }

            return Ok(like.ToLikeDto());
        }



        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var like = await _likeRepository.DeleteAsync(Id);

            if (like == null)
            {
                return NotFound("Like does not exist");
            }

            return Ok(like);
        }
    }
}
