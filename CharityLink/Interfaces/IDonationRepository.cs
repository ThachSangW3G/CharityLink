using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IDonationRepository
    {
        Task<List<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int Id);
        Task<Donation> CreateAsync(Donation donation);
        Task<Donation?> UpdateAsync(int Id, Donation donation);
        Task<Donation?> DeleteAsync(int Id);
        Task<int> GetDonationCount(int CommunityId);
        Task<List<Donation>> GetContributor(int CommunityId);
        Task<int> GetCountDonationByUser (int userId);
        Task<bool> ExistDonationWithUser(int CommunityId, int UserId);
    }
}
