using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public PostRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<Post> CreateAsync(Post Post)
        {
            await _dBContext.Posts.AddAsync(Post);
            await _dBContext.SaveChangesAsync();
            return Post;
        }

        public async Task<Post?> DeleteAsync(int Id)
        {
            var Post = await _dBContext.Posts.FirstOrDefaultAsync(c => c.PostId == Id);
            if (Post == null)
            {
                return null;
            }

            _dBContext.Posts.Remove(Post);
            await _dBContext.SaveChangesAsync();
            return Post;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _dBContext.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int Id)
        {
            return await _dBContext.Posts.FirstOrDefaultAsync(c => c.PostId == Id);
        }

        public async Task<int> GetCountPostByUser(int userId)
        {
            return await _dBContext.Posts.Where(p => p.UserId == userId).CountAsync();
        }

        public async Task<List<Post>> GetPostByCommunity(int CommunityId)
        {
            return await _dBContext.Posts.Where(p => p.CommunityID == CommunityId).ToListAsync();
        }

        public async Task<List<Post>> GetPostByUser(int userId)
        {
            return await _dBContext.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Post?> UpdateAsync(int Id, Post Post)
        {
            var existingPost = await _dBContext.Posts.FindAsync(Id);
            if (existingPost == null)
            {
                return null;
            }
            existingPost.Title = Post.Title;
            existingPost.Content = Post.Content;
            existingPost.CommunityID = Post.CommunityID;
            existingPost.UserId = Post.UserId;
            existingPost.createDate = Post.createDate;

            await _dBContext.SaveChangesAsync();
            return existingPost;
        }
    }
}
