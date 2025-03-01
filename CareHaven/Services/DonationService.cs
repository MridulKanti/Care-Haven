using CareHaven.Data;
using CareHaven.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareHaven.Services
{
    public class DonationService : IDonationService
    {
        private readonly ApplicationDbContext _context;

        public DonationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Donation>> GetAllDonations()
        {
            return await _context.Donations.ToListAsync();
        }

        public async Task<IEnumerable<Donation>> GetDonationsByUserId(int userId)
        {
            return await _context.Donations.Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<(bool, string)> AddDonation(Donation donation, int userId) // Update method signature
        {
            var orphanage = await _context.Orphanages.FindAsync(donation.OrphanageId);
            if (orphanage == null || orphanage.Status != "Active")
            {
                return (false, "Orphanage is inactive or does not exist, cannot donate");
            }

            donation.UserId = userId; // Set the user ID from the method parameter
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
            return (true, "Donation added successfully");
        }

        public async Task<IEnumerable<Donation>> GetDonationsByOrphanageId(int orphanageId)
        {
            return await _context.Donations.Where(d => d.OrphanageId == orphanageId).ToListAsync();
        }

        public async Task<bool> DeleteDonationsByOrphanageId(int orphanageId)
        {
            var donations = await _context.Donations.Where(d => d.OrphanageId == orphanageId).ToListAsync();
            if (donations.Any())
            {
                _context.Donations.RemoveRange(donations);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
