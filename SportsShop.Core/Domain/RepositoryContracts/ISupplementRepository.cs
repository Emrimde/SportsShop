using Entities.Models;

namespace RepositoryContracts
{
    public interface ISupplementRepository
    {
        IQueryable<Supplement> GetAllSupplements();
        Task<Supplement?> GetSupplementById(int id);
    }
}
