using Entities.Models;

namespace ServiceContracts.Interfaces.ISupplier
{
    public interface ISupplierGetterService
    {
        Task<List<Supplier>> GetAllSuppliers();
        Task<decimal> GetSupplierPriceById(int id);
    }
}
