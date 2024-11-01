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

            var postDto = posts.Select(c => c.ToPostDto());

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

            return Ok(post.ToPostDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePostRequestDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = postDto.ToPostFromCreateDTO();

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
