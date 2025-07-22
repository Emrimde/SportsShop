using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface ISupplierRepository
{
    IQueryable<Supplier> GetAllSuppliers();
    Task<decimal> GetSupplierPriceById(int id);
}
