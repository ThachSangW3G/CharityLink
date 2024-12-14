using CharityLink.Data;
using CharityLink.Dtos.Communities;
using CharityLink.Dtos.Posts;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IPostRepository _postRepository;

        public PostController(ApplicationDBContext applicationDBContext, IPostRepository postRepository)
        {
            _applicationDBContext = applicationDBContext;
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var posts = await _postRepository.GetAllAsync();

            var postDto = posts.Select(post =>
            {
                var dto = post.ToPostDto();
                if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    dto.ImageUrl = $"{Request.Scheme}://{Request.Host}{dto.ImageUrl}";
                }

                return dto;
            });

            return Ok(postDto);
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Post>> GetPost([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepository.GetByIdAsync(Id);
            if (post == null)
            {
                return NotFound();
            }

            var postDto = post.ToPostDto();

            if (!string.IsNullOrEmpty(postDto.ImageUrl))
            {
                postDto.ImageUrl = $"{Request.Scheme}://{Request.Host}{postDto.ImageUrl}";
            }

            return Ok(post.ToPostDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreatePostRequestDto postDto, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string imageUrl = string.Empty;
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "image_posts");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageUrl = $"/image_posts/{uniqueFileName}";
            }


            var post = postDto.ToPostFromCreateDTO();
            post.ImageUrl = imageUrl;

            await _postRepository.CreateAsync(post);

            return Ok();


        }



        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromForm] UpdatePostRequestDto postRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepository.UpdateAsync(Id, postRequestDto.ToPostFromUpdateDTO());

            if (post == null)
            {
                return NotFound("Post not found");

            }

            return Ok(post.ToPostDto());
        }



        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepository.DeleteAsync(Id);

            if (post == null)
            {
                return NotFound("Post does not exist");
            }

            return Ok(post);
        }

    }
}
