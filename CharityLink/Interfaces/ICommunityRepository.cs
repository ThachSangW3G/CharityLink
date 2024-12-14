using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface ICommunityRepository
    {
        Task<List<Community>> GetAllAsync ();
        Task<Community?> GetByIdAsync(int Id);
        Task<Community> CreateAsync(Community community);
        Task<Community?> UpdateAsync(int Id, Community community);
        Task<Community?> DeleteAsync(int Id);

        Task<List<Post>> GetPostByCommunityId(int CommunityId);
        Task<List<Donation>> GetDonationByCommunityId(int CommunityId);
        Task<decimal> GetAmountDonationForCommunity(int CommunityId);
        Task<int> GetDonationCount(int CommunityId);

        Task<List<Community>> GetUpComing();
        Task<List<Community>> GetOnGoing();
        Task<List<Community>> GetCompleted();
    }
}
