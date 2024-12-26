using CharityLink.Data;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityLink.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ApplicationDBContext _dBContext;

        public DonationRepository(ApplicationDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public async Task<Donation> CreateAsync(Donation donation)
        {
            await _dBContext.Donations.AddAsync(donation);
            await _dBContext.SaveChangesAsync();
            return donation;
        }

        public async Task<Donation?> DeleteAsync(int Id)
        {

            var donation = await _dBContext.Donations.FirstOrDefaultAsync(c => c.DonationId == Id);
            if (donation == null)
            {
                return null;
            }

            _dBContext.Donations.Remove(donation);
            await _dBContext.SaveChangesAsync();
            return donation;
        }

        public async Task<List<Donation>> GetAllAsync()
        {
            return await _dBContext.Donations.ToListAsync();
        }

        public async Task<Donation?> GetByIdAsync(int Id)
        {
            return await _dBContext.Donations.FirstOrDefaultAsync(c => c.DonationId == Id);
        }

        public async Task<List<Donation>> GetContributor(int CommunityId)
        {
            return await _dBContext.Donations
                .Where(d => d.CommunityId == CommunityId)
                .GroupBy(d => d.UserId)
                .Select(g => new Donation
                {
                    UserId = g.Key,
                    Amount = g.Sum(d => d.Amount),
                })
                .OrderByDescending(c => c.Amount).ToListAsync();
        }

        public async Task<int> GetCountDonationByUser(int userId)
        {
            return await _dBContext.Donations.Where(d => d.UserId == userId).CountAsync();
        }

        public async Task<int> GetDonationCount(int CommunityId)
        {
            return await _dBContext.Donations.CountAsync(d => d.CommunityId == CommunityId);
        }

        public async Task<Donation?> UpdateAsync(int Id, Donation donation)
        {
            var existingDonation = await _dBContext.Donations.FindAsync(Id);
            if (existingDonation == null)
            {
                return null;
            }
            existingDonation.CommunityId = donation.CommunityId;
            existingDonation.UserId = donation.UserId;
            existingDonation.Amount = donation.Amount;
            existingDonation.donateDate = donation.donateDate;

            await _dBContext.SaveChangesAsync();
            return existingDonation;
        }
    }
}
