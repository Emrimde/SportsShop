using Entities.Models;

namespace ServiceContracts.Interfaces.ISupplement
{
    public interface ISupplementGetterService
    {
        Task<List<Supplement>> GetAllSupplements();
        Task<Supplement> GetSupplement(int id);
        Task<List<Supplement>> FilterSupplement(string type, string flavor);
    }
}
