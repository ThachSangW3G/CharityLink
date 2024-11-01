using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int Id);
        Task<Post> CreateAsync(Post Post);
        Task<Post?> UpdateAsync(int Id, Post Post);
        Task<Post?> DeleteAsync(int Id);
    }
}
