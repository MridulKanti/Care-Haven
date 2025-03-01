using CareHaven.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareHaven.Services
{
    public interface IDonationService
    {
        Task<IEnumerable<Donation>> GetAllDonations();
        Task<IEnumerable<Donation>> GetDonationsByUserId(int userId);
        Task<(bool, string)> AddDonation(Donation donation, int userId); // Update method signature
        Task<IEnumerable<Donation>> GetDonationsByOrphanageId(int orphanageId);
        Task<bool> DeleteDonationsByOrphanageId(int orphanageId);
    }
}