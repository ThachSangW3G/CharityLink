using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface ILikeRepository
    {
        Task<List<Like>> GetAllAsync();
        Task<Like?> GetByIdAsync(int Id);
        Task<Like> CreateAsync(Like Like);
        Task<Like?> UpdateAsync(int Id, Like Like);
        Task<Like?> DeleteAsync(int Id);
    }
}
