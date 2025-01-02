using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface ICommunityRepository
    {
        Task<List<Community>> GetAllAsync ();
        //for Admin
        Task<List<Community>> GetAllNoPublic ();

        Task<List<Community>> GetCommunitiesRejected ();
        Task<Community?> GetByIdAsync(int Id);
        Task<Community> CreateAsync(Community community);
        Task<Community?> UpdateAsync(int Id, Community community);
        Task<Community?> DeleteAsync(int Id);

        Task<bool> UpdatePublishStatusAsync(int Id, PublishStatus publishStatus);

        Task<List<Post>> GetPostByCommunityId(int CommunityId);
        Task<List<Donation>> GetDonationByCommunityId(int CommunityId);
        Task<decimal> GetAmountDonationForCommunity(int CommunityId);
        Task<int> GetDonationCount(int CommunityId);

        Task<List<Community>> GetCommunitiesByAdminId(int AdminId);

        //for User
        Task<List<Community>> GetCommunitiesByAdminIdNoPublic(int AdminId);

        Task<List<Community>> GetCommunitiesByAdminIdRejected(int AdminId);

        Task<List<Community>> GetUpComing();
        Task<List<Community>> GetOnGoing();
        Task<List<Community>> GetCompleted();
    }
}
