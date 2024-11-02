using CharityLink.Data;
using CharityLink.Dtos.Comments;
using CharityLink.Dtos.Posts;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ApplicationDBContext applicationDBContext, ICommentRepository commentRepository)
        {
            _applicationDBContext = applicationDBContext;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var comments = await _commentRepository.GetAllAsync();

            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Post>> GetComment([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(Id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = commentDto.ToCommentFromCreateDTO();

            await _commentRepository.CreateAsync(comment);

            return Ok();


        }



        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromForm] UpdateCommentRequestDto commentRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.UpdateAsync(Id, commentRequestDto.ToCommentFromUpdateDTO());

            if (comment == null)
            {
                return NotFound("Comment not found");

            }

            return Ok(comment.ToCommentDto());
        }



        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.DeleteAsync(Id);

            if (comment == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok(comment);
        }

        [HttpGet("{PostId:int}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPostId(int PostId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var comments = await _commentRepository.GetCommentsByPostId(PostId);

            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }
    }
}
