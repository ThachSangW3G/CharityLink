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
    }
}
