using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface ISupplementsService
    {
        Task<List<Supplement>> GetAllSupplements();
        Task<Supplement> GetSupplement(int id);
        Task<List<Supplement>> FilterSupplement(string type, string flavor);
    }
}
