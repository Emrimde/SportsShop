using Entities.Models;

namespace RepositoryContracts
{
    public interface IGymnasticRingRepository
    {
        Task<IEnumerable<GymnasticRing>> GetAllGymnasticRings();
        Task<GymnasticRing?> GetGymnasticRingById(int id);
    }
}
