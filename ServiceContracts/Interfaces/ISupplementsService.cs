using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface ISupplementsService
    {
        Task<List<Supplement>> GetAllSupplements();
        Task<Supplement> GetSupplement(int id);
    }
}
