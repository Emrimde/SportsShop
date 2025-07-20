using Entities.Models;
namespace RepositoryContracts
{
    public interface ISupplierRepository
    {
        IQueryable<Supplier> GetAllSuppliers();
        Task<decimal> GetSupplierPriceById(int id);
    }
}
