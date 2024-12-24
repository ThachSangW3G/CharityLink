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
        private readonly IUserRepository _userRepository;

        public CommentController(ApplicationDBContext applicationDBContext, ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _applicationDBContext = applicationDBContext;
            _commentRepository = commentRepository;
            _userRepository=userRepository;
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

            return Ok(comment.ToCommentDto());


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

            var UpdatedCommentDto = new List<CommentDto>();
            
            foreach (var comment in commentDto)
            {
                var user = await _userRepository.GetByIdAsync(comment.UserId);

                if (user != null)
                {
                    comment.UserName = user.Name;
                    comment.AvatarUrl = user.AvatarUrl;
                }
                
                UpdatedCommentDto.Add(comment);
            }

            return Ok(UpdatedCommentDto);
        }

        [HttpGet("get-parent-comment-by-postId/{PostId:int}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetParentCommentsByPostId(int PostId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetParentCommentByPostId(PostId);
            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }


        [HttpGet("get-children-comment-by-parentId/{ParentCommentId:int}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetChildrenCommentsByParentCommentId(int ParentCommentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetChildrenCommentByParentId(ParentCommentId);
            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }
    }
}
