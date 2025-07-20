using ServiceContracts.DTO.GymnasticRingDto;

namespace ServiceContracts.Interfaces.IGymnasticRing
{
    public interface IGymnasticRingGetterService
    {
        Task<IReadOnlyList<GymnasticRingResponse>> GetAllGymnasticRings();
        Task<GymnasticRingResponse?> GetGymnasticRingById(int id);
    }
}
