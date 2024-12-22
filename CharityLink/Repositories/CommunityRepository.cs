using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repository
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public CommunityRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }
        public async Task<Community> CreateAsync(Community community)
        {
            await _dBContext.Communities.AddAsync(community);
            await _dBContext.SaveChangesAsync();
            return community;
        }

        public async Task<Community?> DeleteAsync(int Id)
        {
            var community = await _dBContext.Communities.FirstOrDefaultAsync(c => c.CommunityId == Id);
            if (community == null)
            {
                return null;
            }

            _dBContext.Communities.Remove(community);
            await _dBContext.SaveChangesAsync();
            return community;
        }

        public async Task<List<Community>> GetAllAsync()
        {
            return await _dBContext.Communities.Where(c=> c.IsPublished == true).ToListAsync();
        }

        public async Task<List<Community>> GetAllNoPublic()
        {
            return await _dBContext.Communities.Where(c=> c.IsPublished == false).ToListAsync();
        }

        public async Task<decimal> GetAmountDonationForCommunity(int CommunityId)
        {
            return await _dBContext.Donations
                .Where(d => d.CommunityId == CommunityId)
                .SumAsync(d => d.Amount);
        }

        public async Task<Community?> GetByIdAsync(int Id)
        {
            return await _dBContext.Communities.FirstOrDefaultAsync(c => c.CommunityId == Id);
        }

        public async Task<List<Community>> GetCompleted()
        {
            var currentDate = DateTime.Now;
            return await _dBContext.Communities.Where(c => c.EndDate < currentDate).ToListAsync();
        }

        public async Task<List<Donation>> GetDonationByCommunityId(int CommunityId)
        {
            return await _dBContext.Donations.Where(d => d.CommunityId == CommunityId).ToListAsync();
        }

        public async Task<int> GetDonationCount(int CommunityId)
        {
            return await _dBContext.Donations.Where(d => d.CommunityId == CommunityId).CountAsync();
        }

        public async Task<List<Community>> GetOnGoing()
        {
            var currentDate = DateTime.Now;
            return await _dBContext.Communities.Where(c => c.StartDate <= currentDate && c.EndDate >= currentDate).ToListAsync();
        }

        public async Task<List<Post>> GetPostByCommunityId(int CommunityId)
        {
            return await _dBContext.Posts.Where(p => p.CommunityID == CommunityId).ToListAsync();
        }

        public async Task<List<Community>> GetUpComing()
        {
            var currentDate = DateTime.Now;
            return await _dBContext.Communities.Where(c => c.StartDate > currentDate).ToListAsync();
        }

        public async Task<Community?> UpdateAsync(int Id, Community community)
        {
            var existingCommunity = await _dBContext.Communities.FindAsync(Id);
            if (existingCommunity == null)
            {
                return null;
            }
            existingCommunity.CommunityName = community.CommunityName;
            existingCommunity.Description = community.Description;
            existingCommunity.IsPublished = community.IsPublished;
            existingCommunity.AdminId = community.AdminId;  
            existingCommunity.CreateDate = community.CreateDate;

            await _dBContext.SaveChangesAsync();
            return existingCommunity;
        }
    }
}
