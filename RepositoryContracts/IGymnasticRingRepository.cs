using Entities.Models;

namespace RepositoryContracts
{
    public interface IGymnasticRingRepository
    {
        IQueryable<GymnasticRing> GetAllGymnasticRings();
        Task<GymnasticRing?> GetGymnasticRingById(int id);
    }
}
