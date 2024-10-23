using CharityLink.Data;
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

        public CommunityController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetCommunites()
        {
            if (_applicationDBContext.Communities == null)
            {
                return NotFound();
            }

            var communities = await _applicationDBContext.Communities.ToListAsync();

            var communityDto = communities.Select(c => c.ToCommunityDto());

            return Ok(communityDto);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Community>> GetCommunity(int id)
        {
            if (_applicationDBContext.Communities == null)
            {
                return NotFound();  
            }
            var community = await _applicationDBContext.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            return  Ok(community.ToCommunityDto());
        }







       
    }
}
 