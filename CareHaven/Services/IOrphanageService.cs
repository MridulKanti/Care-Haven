using CareHaven.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareHaven.Services
{
    public interface IOrphanageService
    {
        Task<IEnumerable<Orphanage>> GetAllOrphanages();
        Task<Orphanage> GetOrphanageById(int orphanageId);
        Task<bool> AddOrphanage(Orphanage orphanage);
        Task<bool> UpdateOrphanage(int orphanageId, Orphanage orphanage);
        Task<bool> DeleteOrphanage(int orphanageId);
    }
}