﻿using CharityLink.Models;

namespace CharityLink.Interfaces
{
    public interface IDonationRepository
    {
        Task<List<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int Id);
        Task<Donation> CreateAsync(Donation donation);
        Task<Donation?> UpdateAsync(int Id, Donation donation);
        Task<Donation?> DeleteAsync(int Id);
    }
}
