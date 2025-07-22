using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface IGymnasticRingRepository
{
    Task<IEnumerable<GymnasticRing>> GetAllGymnasticRings();
    Task<GymnasticRing?> GetGymnasticRingById(int id);
}
