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
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ICommunityRepository _communityRepository;

        public PostController(ApplicationDBContext applicationDBContext, IPostRepository postRepository, ILikeRepository likeRepository, ICommentRepository commentRepository, IConfiguration configuration, IUserRepository userRepository, ICommunityRepository communityRepository)
        {
            _applicationDBContext = applicationDBContext;
            _postRepository = postRepository;
            _likeRepository=likeRepository;
            _commentRepository=commentRepository;
            _configuration=configuration;
            _userRepository=userRepository;
            _communityRepository=communityRepository;
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
                return dto;
            });

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            var UpdatePostDtoList = new List<PostDto>();

            foreach (var post in postDto)
            {
                post.LikeCount = await _likeRepository.CountLike(post.PostId);
                post.CommentCount = await _commentRepository.CountComment(post.PostId);
                var community = await _communityRepository.GetByIdAsync(post.CommunityID);

                post.CommunityName = community.CommunityName;

                var user = await _userRepository.GetByIdAsync(post.PostId);

                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    post.ImageUrl = $"{baseUrl}{post.ImageUrl}"; 
                }

                if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    post.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";
                    
                }
                if (user != null && !string.IsNullOrEmpty(user.Name))
                {
                    post.UserName = user.Name;
                }

                UpdatePostDtoList.Add(post);
            }

            return Ok(UpdatePostDtoList);
        }


        [HttpGet("{Id:int}/{userId:int}")]
        public async Task<ActionResult<Post>> GetPost([FromRoute] int Id, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepository.GetByIdAsync(Id);
            if (post == null)
            {
                return NotFound();
            }

            var postDto = post.ToPostDto();
            postDto.LikeCount = await _likeRepository.CountLike(post.PostId);
            postDto.CommentCount = await _commentRepository.CountComment(post.PostId);
            postDto.IsLiked = await _likeRepository.HasLikesByPostId(userId, post.PostId);

            var community = await _communityRepository.GetByIdAsync(post.CommunityID);

            postDto.CommunityName = community.CommunityName;

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";



            if (!string.IsNullOrEmpty(postDto.ImageUrl))
            {
                postDto.ImageUrl = $"{baseUrl}{post.ImageUrl}";
            }

            var user = await _userRepository.GetByIdAsync(post.PostId);


            if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
            {
                postDto.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";

            }
            if (user != null && !string.IsNullOrEmpty(user.Name))
            {
                postDto.UserName = user.Name;
            }

            return Ok(postDto);
        }


        [HttpGet("get-posts-by-community/{communityId:int}/{userId:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByCommunity([FromRoute]  int communityId, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _postRepository.GetPostByCommunity(communityId);
            var postDto = posts.Select(post =>
            {
                var dto = post.ToPostDto();
                return dto;
            });

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            var UpdatePostDtoList = new List<PostDto>();

            foreach (var post in postDto)
            {
                post.LikeCount = await _likeRepository.CountLike(post.PostId);
                post.CommentCount = await _commentRepository.CountComment(post.PostId);
                post.IsLiked = await _likeRepository.HasLikesByPostId(userId, post.PostId);

                var community = await _communityRepository.GetByIdAsync(post.CommunityID);

                post.CommunityName = community.CommunityName;

                var user = await _userRepository.GetByIdAsync(post.UserId);

                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    post.ImageUrl = $"{baseUrl}{post.ImageUrl}";
                }

                if (user != null && !string.IsNullOrEmpty(user.Name))
                {
                    post.UserName = user.Name;
                }

                if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    post.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";
                    
                }

                UpdatePostDtoList.Add(post);
            }

            return Ok(UpdatePostDtoList);
        }

        [HttpGet("get-posts-by-user/{userId:int}/{myId:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUser([FromRoute] int userId, int myId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var posts = await _postRepository.GetPostByUser(userId);
            var postDto = posts.Select(post =>
            {
                var dto = post.ToPostDto();
                return dto;
            });

            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            var UpdatePostDtoList = new List<PostDto>();

            foreach (var post in postDto)
            {
                post.LikeCount = await _likeRepository.CountLike(post.PostId);
                post.CommentCount = await _commentRepository.CountComment(post.PostId);
                post.IsLiked = await _likeRepository.HasLikesByPostId(myId, post.PostId);

                var community = await _communityRepository.GetByIdAsync(post.CommunityID);

                post.CommunityName = community.CommunityName;

                var user = await _userRepository.GetByIdAsync(post.UserId);

                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    post.ImageUrl = $"{baseUrl}{post.ImageUrl}";
                }

                if (user != null && !string.IsNullOrEmpty(user.Name))
                {
                    post.UserName = user.Name;
                }

                if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    post.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";

                }

                UpdatePostDtoList.Add(post);
            }

            return Ok(UpdatePostDtoList);
        }



        [HttpPost]
        public async Task<ActionResult<Post>> Create([FromForm] CreatePostRequestDto postDto, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

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


            var postResult = await _postRepository.CreateAsync(post);

            var postReturn = postResult.ToPostDto();

            var user = await _userRepository.GetByIdAsync(post.UserId);

            if (!string.IsNullOrEmpty(postReturn.ImageUrl))
            {
                postReturn.ImageUrl = $"{baseUrl}{postReturn.ImageUrl}";
            }

            if (user != null && !string.IsNullOrEmpty(user.Name))
            {
                postReturn.UserName = user.Name;
            }

            if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
            {
               postReturn.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";

            }

            return Ok(postReturn);

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
