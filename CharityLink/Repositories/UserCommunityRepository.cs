using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class UserCommunityRepository : IUserCommunityRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public UserCommunityRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<bool> CheckExist(int userId, int communityId)
        {
            return await _dBContext.UserCommunities
                .AnyAsync(uc => uc.UserId == userId && uc.CommunityId == communityId);
        }

        public async Task<UserCommunity> CreateAsync(UserCommunity userCommunity)
        {
            await _dBContext.UserCommunities.AddAsync(userCommunity);
            await _dBContext.SaveChangesAsync();
            return userCommunity;
        }

        public async Task<UserCommunity?> DeleteAsync(int userId, int communityId)
        {
            var userCommnunity = await _dBContext.UserCommunities.FirstOrDefaultAsync(c => c.UserId == userId && c.CommunityId == communityId);
            if (userCommnunity == null)
            {
                return null;
            }

            _dBContext.UserCommunities.Remove(userCommnunity);
            await _dBContext.SaveChangesAsync();
            return userCommnunity;
        }

        public async Task<List<UserCommunity>> GetAllAsync()
        {
            return await _dBContext.UserCommunities.ToListAsync();
        }

        public async Task<UserCommunity?> GetByIdAsync(int userId, int communityId)
        {
            return await _dBContext.UserCommunities.FirstOrDefaultAsync(c => c.UserId == userId && c.CommunityId == communityId);
        }

        public async Task<List<Community>> GetCommunityByUserId(int userId)
        {
            return await _dBContext.UserCommunities
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Community)
                .ToListAsync();
        }

        public async Task<List<User>> GetUserByCommunityId(int communityId)
        {
            return await _dBContext.UserCommunities
                .Where(uc => uc.CommunityId == communityId)
                .Select(uc => uc.User)
                .ToListAsync();
        }
    }
}
