using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public UserRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<User> CreateAsync(User User)
        {
            await _dBContext.Users.AddAsync(User);
            await _dBContext.SaveChangesAsync();
            return User;
        }

        public async Task<User?> DeleteAsync(int Id)
        {
            var User = await _dBContext.Users.FirstOrDefaultAsync(c => c.UserId == Id);
            if (User == null)
            {
                return null;
            }

            _dBContext.Users.Remove(User);
            await _dBContext.SaveChangesAsync();
            return User;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dBContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int Id)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(c => c.UserId == Id);
        }

        public async Task<User?> UpdateAsync(int Id, User User)
        {
            var existingUser = await _dBContext.Users.FindAsync(Id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Name = User.Name;
            existingUser.Email = User.Email;
            existingUser.Password = User.Password;
            existingUser.AvatarUrl = User.AvatarUrl;
            existingUser.PhoneNumber = User.PhoneNumber;

            await _dBContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
