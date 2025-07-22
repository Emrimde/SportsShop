using SportsShop.Core.ServiceContracts.DTO.GymnasticRingDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.IGymnasticRing;
public interface IGymnasticRingGetterService
{
    Task<IReadOnlyList<GymnasticRingResponse>> GetAllGymnasticRings();
    Task<GymnasticRingResponse?> GetGymnasticRingById(int id);
}
