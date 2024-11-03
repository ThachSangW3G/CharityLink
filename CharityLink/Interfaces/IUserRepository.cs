using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int Id);
        Task<User> CreateAsync(User User);
        Task<User?> UpdateAsync(int Id, User User);
        Task<User?> DeleteAsync(int Id);
    }
}
