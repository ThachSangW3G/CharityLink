using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public LikeRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<int> CountLike(int PostId)
        {
            return await _dBContext.Likes.Where(l => l.PostId == PostId).CountAsync();
        }

        public async Task<Like> CreateAsync(Like Like)
        {
            await _dBContext.Likes.AddAsync(Like);
            await _dBContext.SaveChangesAsync();
            return Like;
        }

        public async Task<Like?> DeleteAsync(int Id)
        {
            var Like = await _dBContext.Likes.FirstOrDefaultAsync(c => c.LikeId == Id);
            if (Like == null)
            {
                return null;
            }

            _dBContext.Likes.Remove(Like);
            await _dBContext.SaveChangesAsync();
            return Like;
        }

        public async Task<List<Like>> GetAllAsync()
        {
            return await _dBContext.Likes.ToListAsync();
        }

        public async Task<Like?> GetByIdAsync(int Id)
        {
            return await _dBContext.Likes.FirstOrDefaultAsync(c => c.LikeId == Id);
        }

        public async Task<List<Like>> GetLikesByPostId(int PostId)
        {
            return await _dBContext.Likes.Where(c => c.PostId == PostId).ToListAsync();
        }

        public async Task<Like?> UpdateAsync(int Id, Like Like)
        {
            var existingLike = await _dBContext.Likes.FindAsync(Id);
            if (existingLike == null)
            {
                return null;
            }
            existingLike.UserId = Like.UserId;
            existingLike.PostId = Like.PostId;
            existingLike.LikeAt = Like.LikeAt;

            await _dBContext.SaveChangesAsync();
            return existingLike;
        }


        public async Task LikePost(int userId, int postId)
        {
            var existingLike = await _dBContext.Likes.FirstOrDefaultAsync(p => p.PostId == postId && p.UserId == userId);
            if (existingLike != null)
            {
                _dBContext.Remove(existingLike);
                await _dBContext.SaveChangesAsync();
            }else
            {
                var like = new Like
                {
                    UserId = userId,
                    PostId = postId,
                    LikeAt = DateTime.Now,
                };

                 _dBContext.Add(like);
                await _dBContext.SaveChangesAsync();
            }


        }
    }
}
