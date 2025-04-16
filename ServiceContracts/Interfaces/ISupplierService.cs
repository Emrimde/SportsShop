using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllSuppliers();
        Task<decimal> GetSupplierPriceById(int id);
    }
}
