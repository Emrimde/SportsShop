using ServiceContracts.DTO.GymnasticRingDto;

namespace ServiceContracts.Interfaces.IGymnasticRing
{
    public interface IGymnasticRingGetterService
    {
        Task<List<GymnasticRingResponse>> GetAllGymnasticRings();
        Task<GymnasticRingResponse?> GetGymnasticRingById(int id);
    }
}
