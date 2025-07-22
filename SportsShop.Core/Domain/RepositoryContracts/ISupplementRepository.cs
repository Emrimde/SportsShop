using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface ISupplementRepository
{
    IQueryable<Supplement> GetAllSupplements();
    Task<Supplement?> GetSupplementById(int id);
}
