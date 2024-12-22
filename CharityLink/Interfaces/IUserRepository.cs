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
        Task<User?> GetByEmailAsync(string email);
        Task<User?> LoginByEmailAndPassword(string email, string password);
        Task<User?> ChangeAvatar(int userId, string avatarUrl);
    }
}
