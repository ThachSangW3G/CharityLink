using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IUserCommunityRepository
    {
        Task<List<UserCommunity>> GetAllAsync();
        Task<UserCommunity?> GetByIdAsync(int userId, int communityId);
        Task<UserCommunity> CreateAsync(UserCommunity userCommunity);
        Task<UserCommunity?> DeleteAsync(int userId, int communityId);

        Task<List<Community>> GetCommunityByUserId(int userId);
        Task<List<User>> GetUserByCommunityId(int communityId);

        Task<bool> CheckExist(int userId, int communityId);
    }
}
